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
    [Authorize]
    public class SportProductController : Controller
    {
        private ISportProductRepository repository;
        public int pageSize = 4;

        public SportProductController(ISportProductRepository repo)
        {
            repository = repo;
        }

        public ViewResult List(string category, int page = 1)
        {
            ProductsListViewModel model = new ProductsListViewModel
            {
                Products = repository.Products
                    .Where(p => category == null || p.Category == category)
                    .OrderBy(product => product.Id)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = category == null ?
                    repository.Products.Count() :
                    repository.Products.Where(product => product.Category == category).Count()
                },
                CurrentCategory = category
            };
            return View(model);
        }

        public FileContentResult GetImage(int id)
        {
            Product product = repository.Products
                .FirstOrDefault(g => g.Id == id);

            if (product != null)
            {
                return File(product.ImageData, product.ImageMimeType);
            }
            else
            {
                return null;
            }
        }

    }
}

