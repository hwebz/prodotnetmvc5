using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductsRepository _repository;
        public int PageSize = 4;

        public ProductController(IProductsRepository repositoryParam)
        {
            _repository = repositoryParam;
        }

        public ActionResult Index()
        {
            return View("List");
        }

        public ViewResult List(string category, int page = 1)
        {
            /* return View(_repository.Products
                        .OrderBy(p => p.ProductID)
                        .Skip((page - 1) * PageSize)
                        .Take(PageSize)); */
            var model = new ProductsListViewModel
            {
                Products = _repository.Products.Where(p => category == null || p.Category == category).OrderBy(p => p.ProductId).Skip((page - 1) * PageSize).Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ? _repository.Products.Count() : _repository.Products.Count(e => e.Category == category)
                },
                CurrentCategory = category
            };

            return View(model);
        }

        public FileContentResult GetImage(int productId)
        {
            var product = _repository.Products.FirstOrDefault(p => p.ProductId == productId);
            return product != null ? File(product.ImageData, product.ImageMimeType) : null;
        }
    }
}