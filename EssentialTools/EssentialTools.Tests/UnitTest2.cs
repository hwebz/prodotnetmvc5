using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EssentialTools.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace EssentialTools.Tests
{
    [TestClass]
    public class UnitTest2
    {
        private Product[] _products =
        {
            new Product() {Name = "Kayak", Category = "Watersports", Price = 275M},
            new Product() {Name = "Lifejacket", Category = "Watersports", Price = 48.95M},
            new Product() {Name = "Soccer ball", Category = "Soccer", Price = 19.50M},
            new Product() {Name = "Corner flag", Category = "Soccer", Price = 34.95M}
        };

        [TestMethod]
        /*public void Sum_Products_Correctly()
        {
            // arrange
            var discounter = new MinimumDiscountHelper();
            var target = new LinqValueCalculator(discounter);
            var goalTotal = _products.Sum(e => e.Price);

            // act
            var result = target.ValueProducts(_products);

            // assert
            Assert.AreEqual(goalTotal, result);
        }*/
        public void Sum_Products_Correctly()
        {
            // arrange
           var mock = new Mock<IDiscountHelper>();
            mock.Setup(m => m.ApplyDiscount(It.IsAny<decimal>()))
                .Returns<decimal>(total => total);
            var target = new LinqValueCalculator(mock.Object);

            // act
            var result = target.ValueProducts(_products);

            // assert
            Assert.AreEqual(_products.Sum(e => e.Price), result);
        }

        private Product[] createProduct(decimal value)
        {
            return new[]
            {
                new Product() {Price = value}
            };
        }

        [TestMethod]
        //[ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Pass_Through_Variable_Discounts()
        {
            // arrange
            var mock = new Mock<IDiscountHelper>();
            mock.Setup(m => m.ApplyDiscount(It.IsAny<decimal>()))
                .Returns<decimal>(total => total);
            mock.Setup(m => m.ApplyDiscount(It.Is<decimal>(v => v > 100)))
                .Returns<decimal>(total => (total * 0.9M));
            mock.Setup(m => m.ApplyDiscount(It.IsInRange<decimal>(10, 100, Range.Inclusive)))
                .Returns<decimal>(total => total - 5);
            var target = new LinqValueCalculator(mock.Object);

            // act
            var fiveDollarDiscount = target.ValueProducts(createProduct(5));
            var tenDollarDiscount = target.ValueProducts(createProduct(10));
            var fiftyDollarDiscount = target.ValueProducts(createProduct(50));
            var hundredDollarDiscount = target.ValueProducts(createProduct(100));
            var fiveHundredDollarDiscount = target.ValueProducts(createProduct(500));

            // assert
            Assert.AreEqual(5, fiveDollarDiscount, "$5 Fail");
            Assert.AreEqual(5, tenDollarDiscount, "$10 Fail");
            Assert.AreEqual(45, fiftyDollarDiscount, "$50 Fail");
            Assert.AreEqual(95, hundredDollarDiscount, "$100 Fail");
            Assert.AreEqual(450, fiveHundredDollarDiscount, "$500 Fail");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Pass_Through_Negative_Discount()
        {
            var mock = new Mock<IDiscountHelper>();
            mock.Setup(m => m.ApplyDiscount(It.Is<decimal>(v => v <= 0)))
                .Throws<ArgumentOutOfRangeException>();
            var target = new LinqValueCalculator(mock.Object);

            target.ValueProducts(createProduct(0));
        }
    }
}
