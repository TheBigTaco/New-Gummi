using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GummiBear.Models
{
    [Table ("products")]
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int Cost { get; set; }
        public string Description { get; set; }
        public double AverageRating { get; set; }
        public byte[] Picture { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }

        public override bool Equals(System.Object otherProduct)
        {
            if (!(otherProduct is Product))
            {
                return false;
            }
            else
            {
                Product newProduct = (Product)otherProduct;
                return this.ProductId.Equals(newProduct.ProductId);
            }
        }

        public override int GetHashCode()
        {
            return this.ProductId.GetHashCode();
        }

        public void AverageRatingFinder() 
        {
            int count = 0;
            int total = 0;
            foreach(var review in this.Reviews)
            {
                count++;
                total += review.Rating;
            }
            if(count != 0)
            {
                this.AverageRating = total / count;
            }
            else
            {
                this.AverageRating = 0;
            }
        }
    }
}
