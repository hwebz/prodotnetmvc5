﻿using System;
using System.Collections.Generic;
using System.Text;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;

namespace SportsStore.Domain.Concrete
{
    public class EFProductRepository : IProductsRepository
    {
        private readonly EFDbContext _context;

        public EFProductRepository()
        {
            _context = new EFDbContext();;
        }

        public IEnumerable<Product> Products
        {
            get => _context.Products;
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(value));
            }
        }

        public void SaveProduct(Product product)
        {
            if (product.ProductId == 0)
            {
                _context.Products.Add(product);
            }
            else
            {
                var dbEntry = _context.Products.Find(product.ProductId);
                if (dbEntry != null)
                {
                    dbEntry.Name = product.Name;
                    dbEntry.Description = product.Description;
                    dbEntry.Price = product.Price;
                    dbEntry.Category = product.Category;
                    dbEntry.ImageData = product.ImageData;
                    dbEntry.ImageMimeType = product.ImageMimeType;
                }
            }

            _context.SaveChanges();
        }

        public Product DeleteProduct(int productId)
        {
            var dbEntry = _context.Products.Find(productId);
            if (dbEntry == null) return null;
            _context.Products.Remove(dbEntry);
            _context.SaveChanges();

            return dbEntry;
        }
    }
}
