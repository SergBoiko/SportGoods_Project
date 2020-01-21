using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportGoods.Domain.Abstract;
using SportGoods.Domain.Entities;
using SportGoods.WebUI.Controllers;
using SportGoods.WebUI.Models;

namespace SportGoods.UnitTests
{
    [TestClass]
    public class CartTests
    {
        [TestMethod]
        public void Can_Add_New_Lines()
        {
            // Организация - создание нескольких тестовых товаров
            Product product1 = new Product { Id = 1, Name = "Product1" };
            Product product2 = new Product { Id = 2, Name = "Product2" };

            // Организация - создание корзины
            Cart cart = new Cart();

            // Действие
            cart.AddItem(product1, 1);
            cart.AddItem(product2, 1);
            List<CartLine> results = cart.Lines.ToList();

            // Утверждение
            Assert.AreEqual(results.Count(), 2);
            Assert.AreEqual(results[0].Product, product1);
            Assert.AreEqual(results[1].Product, product2);
        }

        [TestMethod]
        public void Can_Add_Quantity_For_Existing_Lines()
        {
            // Организация - создание нескольких тестовых товаров
            Product product1 = new Product { Id = 1, Name = "Product1" };
            Product product2 = new Product { Id = 2, Name = "Product2" };

            // Организация - создание корзины
            Cart cart = new Cart();

            // Действие
            cart.AddItem(product1, 1);
            cart.AddItem(product2, 1);
            cart.AddItem(product1, 5);
            List<CartLine> results = cart.Lines.OrderBy(c => c.Product.Id).ToList();

            // Утверждение
            Assert.AreEqual(results.Count(), 2);
            Assert.AreEqual(results[0].Quantity, 6);    // 6 экземпляров добавлено в корзину
            Assert.AreEqual(results[1].Quantity, 1);
        }

        [TestMethod]
        public void Can_Remove_Line()
        {
            // Организация - создание нескольких тестовых товаров
            Product product1 = new Product { Id = 1, Name = "Product1" };
            Product product2 = new Product { Id = 2, Name = "Product2" };
            Product product3 = new Product { Id = 3, Name = "Product3" };

            // Организация - создание корзины
            Cart cart = new Cart();

            // Организация - добавление нескольких товаров в корзину
            cart.AddItem(product1, 1);
            cart.AddItem(product2, 4);
            cart.AddItem(product3, 2);
            cart.AddItem(product2, 1);

            // Действие
            cart.RemoveLine(product2);

            // Утверждение
            Assert.AreEqual(cart.Lines.Where(c => c.Product == product2).Count(), 0);
            Assert.AreEqual(cart.Lines.Count(), 2);
        }

        [TestMethod]
        public void Calculate_Cart_Total()
        {
            // Организация - создание нескольких тестовых товаров
            Product product1 = new Product { Id = 1, Name = "Product1", Price = 100 };
            Product product2 = new Product { Id = 2, Name = "Product2", Price = 55 };

            // Организация - создание корзины
            Cart cart = new Cart();

            // Действие
            cart.AddItem(product1, 1);
            cart.AddItem(product2, 1);
            cart.AddItem(product1, 5);
            decimal result = cart.ComputeTotalValue();

            // Утверждение
            Assert.AreEqual(result, 655);
        }

        [TestMethod]
        public void Can_Clear_Contents()
        {
            // Организация - создание нескольких тестовых товаров
            Product product1 = new Product { Id = 1, Name = "Product1", Price = 100 };
            Product product2 = new Product { Id = 2, Name = "Product2", Price = 55 };

            // Организация - создание корзины
            Cart cart = new Cart();

            // Действие
            cart.AddItem(product1, 1);
            cart.AddItem(product2, 1);
            cart.AddItem(product1, 5);
            cart.Clear();

            // Утверждение
            Assert.AreEqual(cart.Lines.Count(), 0);
        }


        /// <summary>
        /// Проверяем добавление в корзину
        /// </summary>
        [TestMethod]
        public void Can_Add_To_Cart()
        {
            // Организация - создание имитированного хранилища
            Mock<ISportProductRepository> mock = new Mock<ISportProductRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product> {
        new Product {Id = 1, Name = "Product1", Category = "Category1"},
            }.AsQueryable());

            // Организация - создание корзины
            Cart cart = new Cart();

            // Организация - создание контроллера
            CartController controller = new CartController(mock.Object);

            // Действие - добавить товар в корзину
            controller.AddToCart(cart, 1, null);

            // Утверждение
            Assert.AreEqual(cart.Lines.Count(), 1);
            Assert.AreEqual(cart.Lines.ToList()[0].Product.Id, 1);
        }

        /// <summary>
        /// После добавления товара в корзину, должно быть перенаправление на страницу корзины
        /// </summary>
        [TestMethod]
        public void Adding_Game_To_Cart_Goes_To_Cart_Screen()
        {
            // Организация - создание имитированного хранилища
            Mock<ISportProductRepository> mock = new Mock<ISportProductRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product> {
        new Product {Id = 1, Name = "Product1", Category = "Category1"},
    }.AsQueryable());

            // Организация - создание корзины
            Cart cart = new Cart();

            // Организация - создание контроллера
            CartController controller = new CartController(mock.Object);

            // Действие - добавить товар в корзину
            RedirectToRouteResult result = controller.AddToCart(cart, 2, "myUrl");

            // Утверждение
            Assert.AreEqual(result.RouteValues["action"], "Index");
            Assert.AreEqual(result.RouteValues["returnUrl"], "myUrl");
        }

        // Проверяем URL
        [TestMethod]
        public void Can_View_Cart_Contents()
        {
            // Организация - создание корзины
            Cart cart = new Cart();

            // Организация - создание контроллера
            CartController target = new CartController(null);

            // Действие - вызов метода действия Index()
            CartIndexViewModel result
                = (CartIndexViewModel)target.Index(cart, "myUrl").ViewData.Model;

            // Утверждение
            Assert.AreSame(result.Cart, cart);
            Assert.AreEqual(result.ReturnUrl, "myUrl");
        }

    }
}
