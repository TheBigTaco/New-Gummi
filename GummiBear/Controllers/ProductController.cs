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

        public IActionResult Edit(int id)
        {
            Product thisProduct = productRepo.Products.FirstOrDefault(x => x.ProductId == id);
            return View(thisProduct);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            productRepo.Edit(product);
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            Product thisProduct = productRepo.Products.Include(product => product.Reviews).FirstOrDefault(x => x.ProductId == id);
            thisProduct.AverageRatingFinder();
            productRepo.Edit(thisProduct);
            return View(thisProduct);
        }

        public IActionResult Delete(int id)
        {
            Product thisProduct = productRepo.Products.FirstOrDefault(x => x.ProductId == id);
            return View(thisProduct);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            Product thisProduct = productRepo.Products.FirstOrDefault(x => x.ProductId == id);
            productRepo.Remove(thisProduct);
            return RedirectToAction("Index");
        }

        public IActionResult GetPicture(int id)
        {
            var thisPicture = productRepo.Products.FirstOrDefault(x => x.ProductId == id).Picture;
            return File(thisPicture, "image/png");
        }
    }
}
