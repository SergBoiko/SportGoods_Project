using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportGoods.Domain.Abstract;
using SportGoods.Domain.Entities;
using SportGoods.WebUI.Controllers;

namespace SportGoods.UnitTests
{
    [TestClass]
    public class ImageTests
    {
        [TestMethod]
        public void Can_Retrieve_Image_Data()
        {
            // Организация - создание объекта Product с данными изображения
            Product product = new Product
            {
                Id = 2,
                Name = "Product2",
                ImageData = new byte[] { },
                ImageMimeType = "image/png"
            };

            // Организация - создание имитированного хранилища
            Mock<ISportProductRepository> mock = new Mock<ISportProductRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product> {
                new Product {Id = 1, Name = "Product1"},
                product,
                new Product {Id = 3, Name = "Product3"}
            }.AsQueryable());

            // Организация - создание контроллера
            SportProductController controller = new SportProductController(mock.Object);

            // Действие - вызов метода действия GetImage()
            ActionResult result = controller.GetImage(2);

            // Утверждение
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(FileResult));
            Assert.AreEqual(product.ImageMimeType, ((FileResult)result).ContentType);
        }

        [TestMethod]
        public void Cannot_Retrieve_Image_Data_For_Invalid_ID()
        {
            // Организация - создание имитированного хранилища
            Mock<ISportProductRepository> mock = new Mock<ISportProductRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product> {
                new Product {Id = 1, Name = "Product1"},
                new Product {Id = 2, Name = "Product2"}
            }.AsQueryable());

            // Организация - создание контроллера
            SportProductController controller = new SportProductController(mock.Object);

            // Действие - вызов метода действия GetImage()
            ActionResult result = controller.GetImage(10);

            // Утверждение
            Assert.IsNull(result);
        }
    }
}

