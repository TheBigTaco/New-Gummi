using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GummiBear.Models;
using GummiBear.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace GummiBear.Tests.ControllerTests
{
    [TestClass]
    public class ProductControllerTests : IDisposable
    {
        Mock<IProductRepository> mock = new Mock<IProductRepository>();
        EFProductRepository db = new EFProductRepository(new TestDbContext());

        public void Dispose()
        {
            db.DeleteAll();
        }

        private void DbSetup()
        {
            mock.Setup(e => e.Products).Returns(new Product[]
            {
                new Product
                {
                    ProductId = 1,
                    Name = "thing",
                    Cost = 3,
                    Description = "it's a thing"
                },
                new Product
                {
                    ProductId = 2,
                    Name = "other thing",
                    Cost = 4,
                    Description = "it's an other thing"
                }
            }.AsQueryable());
        }

        [TestMethod]
        public void Mock_GetViewResultIndex_ActionResult()
        {
            DbSetup();
            ProductController controller = new ProductController(mock.Object);
            var result = controller.Index();

            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }

        [TestMethod]
        public void Mock_IndexContainsModelData_List()
        {
            DbSetup();
            ViewResult indexView = new ProductController(mock.Object).Index() as ViewResult;

            var result = indexView.ViewData.Model;

            Assert.IsInstanceOfType(result, typeof(List<Product>));
        }

        [TestMethod]
        public void Mock_IndexModelContainsProducts_Collection()
        {
            DbSetup();
            ProductController controller = new ProductController(mock.Object);

            Product testProduct = new Product
            {
                ProductId = 1,
                Name = "thing",
                Cost = 3,
                Description = "it's a thing"
            };

            ViewResult indexView = controller.Index() as ViewResult;
            List<Product> collection = indexView.ViewData.Model as List<Product>;

            CollectionAssert.Contains(collection, testProduct);
        }

        [TestMethod]
        public void DB_CreatesNewEntries_Collection()
        {
            ProductController controller = new ProductController(db);
            Product testProduct = new Product
            {
                Name = "thing",
                Cost = 3,
                Description = "it's a thing"
            };

            controller.Create(testProduct, null);
            var collection = (controller.Index() as ViewResult).ViewData.Model as List<Product>;

            CollectionAssert.Contains(collection, testProduct);
        }

        [TestMethod]
        public void Mock_GetDetails_ReturnsView()
        {
            Product testProduct = new Product
            {
                ProductId = 1,
                Name = "thing",
                Cost = 3,
                Description = "it's a thing"
            };

            DbSetup();
            ProductController controller = new ProductController(mock.Object);

            var resultView = controller.Details(testProduct.ProductId) as ViewResult;
            var model = resultView.ViewData.Model as Product;

            Assert.IsInstanceOfType(resultView, typeof(ViewResult));
            Assert.IsInstanceOfType(model, typeof(Product));
        }

        [TestMethod]
        public void DB_DelteSpecificProduct_Collection()
        {
            ProductController controller = new ProductController(db);
            Product testProduct1 = new Product
            {
                Name = "thing",
                Cost = 3,
                Description = "it's a thing"
            };
            Product testProduct2 = new Product
            {
                Name = "other thing",
                Cost = 4,
                Description = "it's an other thing"
            };

            controller.Create(testProduct1, null);
            controller.Create(testProduct2, null);

            var collection = (controller.Index() as ViewResult).ViewData.Model as List<Product>;
            controller.DeleteConfirmed(collection[0].ProductId);
            var collection2 = (controller.Index() as ViewResult).ViewData.Model as List<Product>;

            CollectionAssert.DoesNotContain(collection2, testProduct1);
        }

        [TestMethod]
        public void DB_EditSpecificProduct_Product()
        {
            ProductController controller = new ProductController(db);
            Product testProduct1 = new Product
            {
                Name = "thing",
                Cost = 3,
                Description = "it's a thing"
            };

            controller.Create(testProduct1, null);

            var collection = (controller.Index() as ViewResult).ViewData.Model as List<Product>;

            Product productToEdit = (controller.Edit(collection[0].ProductId) as ViewResult).ViewData.Model as Product;
            productToEdit.Name = "other thing";
            controller.Edit(productToEdit);
            var collection2 = (controller.Index() as ViewResult).ViewData.Model as List<Product>;

            Assert.AreEqual("other thing", collection2[0].Name);
        }
    }
}
