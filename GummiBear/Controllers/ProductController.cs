using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GummiBear.Models;

namespace GummiBear.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository productRepo;
        public ProductController(IProductRepository repo = null)
        {
            if(repo == null)
            {
                this.productRepo = new EFProductRepository();
            }
            else
            {
                this.productRepo = repo;
            }
        }

        public IActionResult Index()
        {
            return View(productRepo.Products.ToList());
        }
    }
}
