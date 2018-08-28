using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;

namespace SportsStore.WebUI.Controllers
{
    public class NavController : Controller
    {
        private IProductsRepository _repository;
        public NavController(IProductsRepository repoParam)
        {
            _repository = repoParam;
        }

        public PartialViewResult Menu(string category = null, bool horizontalLayout = false)
        {
            ViewBag.SelectedCategory = category;

            IEnumerable<string> categories = _repository.Products.Select(x => x.Category).Distinct().OrderBy(x => x);

            string viewName = horizontalLayout ? "MenuHorizontal" : "Menu";

            //return PartialView(viewName, categories);
            return PartialView("FlexMenu", categories); // remove duplication (2 views into 1 view)
        }
    }
}