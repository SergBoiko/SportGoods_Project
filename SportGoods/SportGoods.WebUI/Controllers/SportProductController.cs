using SportGoods.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportGoods.WebUI.Controllers
{
    public class SportProductController : Controller
    {
        private ISportProductRepository repository;
        public int pageSize = 4;

        public SportProductController(ISportProductRepository repo)
        {
            repository = repo;
        }

        public ViewResult List(int page = 1)
        {
            return View(repository.Products
                .OrderBy(product => product.Id)
                .Skip((page - 1)*pageSize)
                .Take(pageSize));
        }
    }
}
