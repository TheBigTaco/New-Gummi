using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GummiBear.Models;
using System.IO;
using Microsoft.EntityFrameworkCore;

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

        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product, IFormFile image)
        {
            byte[] newImage = new byte[0];
            if (image != null)
            {
                using (Stream fileStream = image.OpenReadStream())
                using (MemoryStream ms = new MemoryStream())
                {
                    fileStream.CopyTo(ms);
                    newImage = ms.ToArray();
                }
                product.Picture = newImage;
            }
            else
            {
                Console.WriteLine("No Image");
            }

            productRepo.Save(product);

            return RedirectToAction("Index");
        }
    }
}
