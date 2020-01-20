using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportGoods.Domain.Entities;

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
    }
}
