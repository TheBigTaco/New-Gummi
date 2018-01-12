using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GummiBear.Models
{
    public class EFReviewRepository : IReviewRepository
    {
        GummiBearContext db;
        public EFReviewRepository()
        {
            db = new GummiBearContext();
        }

        public EFReviewRepository(GummiBearContext thisDb)
        {
            db = thisDb;
        }

        public IQueryable<Review> Reviews
        { get { return db.Reviews; } }

        public Review Save(Review review)
        {
            db.Reviews.Add(review);
            db.SaveChanges();
            return review;
        }

        public Review Edit(Review review)
        {
            db.Entry(review).State = EntityState.Modified;
            db.SaveChanges();
            return review;
        }

        public void Remove(Review review)
        {
            db.Reviews.Remove(review);
            db.SaveChanges();
        }

        public void DeleteAll()
        {
            db.Database.ExecuteSqlCommand("DELETE FROM reviews");
        }
    }
}
