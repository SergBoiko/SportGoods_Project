using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportGoods.Domain.Abstract;
using SportGoods.Domain.Entities;
using SportGoods.WebUI.Controllers;
using SportGoods.WebUI.HtmlHelpers;
using SportGoods.WebUI.Models;

namespace SportGoods.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Can_Paginate()
        {
            // Организация (arrange)
            Mock<ISportProductRepository> mock = new Mock<ISportProductRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product>
            {
                new Product { Id = 1, Name = "Product1"},
                new Product { Id = 2, Name = "Product2"},
                new Product { Id = 3, Name = "Product3"},
                new Product { Id = 4, Name = "Product4"},
                new Product { Id = 5, Name = "Product5"}
            });

            SportProductController controller = new SportProductController(mock.Object);
            controller.pageSize = 3;

            // Действие (act)
            ProductsListViewModel result = (ProductsListViewModel)controller.List(2).Model;

            // Утверждение (assert)
            List<Product> products = result.Products.ToList();
            Assert.IsTrue(products.Count == 2);
            Assert.AreEqual(products[0].Name, "Product4");
            Assert.AreEqual(products[1].Name, "Product5");
        }

        [TestMethod]
        public void Can_Generate_Page_Links()
        {

            // Организация - определение вспомогательного метода HTML - это необходимо
            // для применения расширяющего метода
            HtmlHelper myHelper = null;

            // Организация - создание объекта PagingInfo
            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };

            // Организация - настройка делегата с помощью лямбда-выражения
            Func<int, string> pageUrlDelegate = i => "Page" + i;

            // Действие
            MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);

            // Утверждение
            Assert.AreEqual(@"<a class=""btn btn-default"" href=""Page1"">1</a>"
                + @"<a class=""btn btn-default btn-primary selected"" href=""Page2"">2</a>"
                + @"<a class=""btn btn-default"" href=""Page3"">3</a>",
                result.ToString());
        }


        [TestMethod]
        public void Can_Send_Pagination_View_Model()
        {
            // Организация (arrange)
            Mock<ISportProductRepository> mock = new Mock<ISportProductRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product>
            {
                new Product { Id = 1, Name = "Product1"},
                new Product { Id = 2, Name = "Product2"},
                new Product { Id = 3, Name = "Product3"},
                new Product { Id = 4, Name = "Product4"},
                new Product { Id = 5, Name = "Product5"}
            });

            SportProductController controller = new SportProductController(mock.Object);
            controller.pageSize = 3;

            // Act
            ProductsListViewModel result
                = (ProductsListViewModel)controller.List(2).Model;

            // Assert
            PagingInfo pageInfo = result.PagingInfo;
            Assert.AreEqual(pageInfo.CurrentPage, 2);
            Assert.AreEqual(pageInfo.ItemsPerPage, 3);
            Assert.AreEqual(pageInfo.TotalItems, 5);
            Assert.AreEqual(pageInfo.TotalPages, 2);
        }
    }
}
