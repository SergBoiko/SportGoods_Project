using log4net;
using Microsoft.AspNet.Identity;
using SportGoods.Domain.Abstract;
using SportGoods.Domain.Entities;
using SportGoods.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportGoods.WebUI.Controllers
{
    public class CartController : Controller
    {
        private ISportProductRepository productRepository;
        private IOrderProcessor orderProcessor;
        private IOrderRepository orderRepository;
        private ILog log;

        
        public CartController(ISportProductRepository productRepository, IOrderRepository orderRepository, IOrderProcessor processor)
        {
            this.productRepository = productRepository;
            this.orderRepository = orderRepository;
            orderProcessor = processor;
            log = LogManager.GetLogger(typeof(CartController));
        }

        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }

        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, but your shopping cart is empty!");
            }

            if (ModelState.IsValid)
            {
                orderProcessor.ProcessOrder(cart, shippingDetails);
                foreach(var cartLine in cart.Lines)
                {
                    for (int i = 0; i < cartLine.Quantity; i++)
                    {
                        try
                        {
                            orderRepository.SaveOrder(cartLine.Product, User.Identity.GetUserId());
                            log.Info($"New order has been created for user with Id {User.Identity.GetUserId()} for product with Id {cartLine.Product.Id}");
                        }
                        catch (Exception ex)
                        {
                            log.Error($"Exception occured while saving order. Exception details: {ex.Message}");
                        }
                        
                    }
                    
                }
                

                cart.Clear();
                return View("Completed");
            }
            else
            {
                return View(shippingDetails);
            }
        }

        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        public RedirectToRouteResult AddToCart(Cart cart, int Id, string returnUrl)
        {
            Product product = productRepository.Products
                .FirstOrDefault(g => g.Id == Id);

            if (product != null)
            {
                cart.AddItem(product, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int Id, string returnUrl)
        {
            Product product = productRepository.Products
                .FirstOrDefault(g => g.Id == Id);

            if (product != null)
            {
                cart.RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

    }
}