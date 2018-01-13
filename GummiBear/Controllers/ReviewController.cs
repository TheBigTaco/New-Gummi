using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GummiBear.Models;
using GummiBear.ViewModels;
using System.IO;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GummiBear.Controllers
{
    public class ReviewController : Controller
    {
        private IReviewRepository reviewRepo;
        public ReviewController(IReviewRepository repo = null)
        {
            if (repo == null)
            {
                this.reviewRepo = new EFReviewRepository();
            }
            else
            {
                this.reviewRepo = repo;
            }
        }
        public IActionResult Create(int id)
        {
            ProductReviews productReviews = new ProductReviews(id);
            return View(productReviews);
        }

        [HttpPost]
        public IActionResult Create(Review review)
        {
            reviewRepo.Save(review);

            return RedirectToAction("Details", "Product", new { id = review.ProductId });
        }
    }
}
