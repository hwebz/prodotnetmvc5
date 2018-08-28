using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EssentialTools.Models
{
    public class LinqValueCalculator : IValueCalculator
    {
        private readonly IDiscountHelper _discounter;
        private static int _counter = 0;

        public LinqValueCalculator(IDiscountHelper discountParam)
        {
            _discounter = discountParam;
            System.Diagnostics.Debug.WriteLine($"Instance {++_counter} created");
        }
        public decimal ValueProducts(IEnumerable<Product> products)
        {
            return _discounter.ApplyDiscount(products.Sum(p => p.Price));
        }
    }
}