using SportGoods.Domain.Abstract;
using SportGoods.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportGoods.WebUI.Controllers
{
    public class AdminController : Controller
    {
        ISportProductRepository repository;
        
        public AdminController(ISportProductRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index()
        {
            return View(repository.Products);
        }

        public ViewResult Edit(int id)
        {
            Product product = repository.Products
                .FirstOrDefault(g => g.Id == id);
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                repository.SaveProduct(product);
                TempData["message"] = string.Format("Changes \"{0}\" have saved", product.Name);
                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }
        }
    }
}