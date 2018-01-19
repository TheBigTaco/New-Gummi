using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GummiBear.Models;

namespace GummiBear.Tests.ModelTests
{
    [TestClass]
    public class ProductTests
    {
        [TestMethod]
        public void Equals_ProductsAreEqual_True()
        {
            Product product = new Product
            {
                ProductId = 0,
                Name = "Strawberry",
                Cost = 4,
                Description = "Red"
            };

            Product productCopy = product;

            Assert.AreEqual(productCopy, product);
        }

        [TestMethod]
        public void GetProperties_ReturnsProductProperties_Strings()
        {
            Product product = new Product
            {
                ProductId = 0,
                Name = "Strawberry",
                Cost = 4,
                Description = "Red"
            };

            Assert.AreEqual(0, product.ProductId);
            Assert.AreEqual("Strawberry", product.Name);
            Assert.AreEqual(4, product.Cost);
            Assert.AreEqual("Red", product.Description);
        }

        [TestMethod]
        public void AverageRatingFinder_CalculatesProductsAverageRating_Double()
        {
            Review review = new Review
            {
                ReviewId = 0,
                Title = "Strawberry",
                Author = "Me",
                ContentBody = "Red",
                Rating = 1,
                ProductId = 0
            };
            Review review2 = new Review
            {
                ReviewId = 0,
                Title = "Strawberry",
                Author = "Me",
                ContentBody = "Red",
                Rating = 5,
                ProductId = 0
            };
			Product product = new Product
			{
				ProductId = 0,
				Name = "Strawberry",
				Cost = 4,
				Description = "Red",
            };
            product.Reviews = new List<Review>{review, review2};
            product.AverageRatingFinder();
            Assert.AreEqual(3, product.AverageRating);
        }
    }
}
