using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using GummiBear.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GummiBear.ViewModels
{
    public class ProductReviews
    {
        private static EFProductRepository productRepo = new EFProductRepository();

        public Review Review { get; set; } = new Review();
        public int ProductId { get; set; }

        public ProductReviews(int productId)
        {
            ProductId = productId;
            Review.ProductId = productId;
        }
    }
}
