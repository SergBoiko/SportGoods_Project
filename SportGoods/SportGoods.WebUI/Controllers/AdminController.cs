using SportGoods.Domain.Abstract;
using SportGoods.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportGoods.WebUI.Controllers
{
    [Authorize]
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
        public ActionResult Edit(Product product, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    product.ImageMimeType = image.ContentType;
                    product.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(product.ImageData, 0, image.ContentLength);
                }

                repository.SaveProduct(product);
                TempData["message"] = string.Format("Changes \"{0}\" have saved", product.Name);
                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }
        }
        public ViewResult Create()
        {
            return View("Edit", new Product());
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            Product deletedProduct = repository.DeleteProduct(id);
            if (deletedProduct != null)
            {
                TempData["message"] = string.Format("Product \"{0}\" has been deleted",
                    deletedProduct.Name);
            }
            return RedirectToAction("Index");
        }

    }
}