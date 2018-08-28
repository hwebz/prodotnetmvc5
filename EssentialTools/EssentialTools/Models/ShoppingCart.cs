using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EssentialTools.Models
{
    public class ShoppingCart
    {
        private readonly IValueCalculator _calc;

        public ShoppingCart(IValueCalculator calcParam)
        {
            _calc = calcParam;
        }

        public IEnumerable<Product> Products { get; set; }

        public decimal CalculateProductTotal()
        {
            return _calc.ValueProducts(Products);
        }
    }
}