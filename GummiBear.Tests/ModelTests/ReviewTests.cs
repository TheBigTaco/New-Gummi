using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GummiBear.Models;

namespace GummiBear.Tests.ModelTests
{
    [TestClass]
    public class ReviewTests
    {
        [TestMethod]
        public void Equals_ReviewsAreEqual_True()
        {
            Review review = new Review
            {
                ReviewId = 0,
                Title = "Strawberry",
                Author = "Me",
                ContentBody = "Red",
                Rating = 4
            };

            Review reviewCopy = review;

            Assert.AreEqual(reviewCopy, review);
        }

        [TestMethod]
        public void GetProperties_ReturnsReviewProperties_Strings()
        {
            Review review = new Review
            {
                ReviewId = 0,
                Title = "Strawberry",
                Author = "Me",
                ContentBody = "Red",
                Rating = 4
            };

            Assert.AreEqual(0, review.ReviewId);
            Assert.AreEqual("Strawberry", review.Title);
            Assert.AreEqual("Me", review.Author);
            Assert.AreEqual("Red", review.ContentBody);
            Assert.AreEqual(4, review.Rating);
        }
    }
}
