﻿using ClassRegistration.DataAccess.Entity;
using ClassRegistration.DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Xunit;

namespace ClassRegistration.Test.DataAccess.Repository
{
    public class ReviewsRepositoryTest
    {
        private readonly ReviewsRepository _reviewsRepository;

        public ReviewsRepositoryTest ()
        {
            var context = new Course_registration_dbContext (

                new DbContextOptionsBuilder<Course_registration_dbContext> ()
                    .UseInMemoryDatabase (databaseName: "Reviews")
                    .Options
            );

            context.Database.EnsureDeleted ();

            context.Add (new Reviews
            {
                ReviewId = 1
            });

            context.SaveChanges ();

            _reviewsRepository = new ReviewsRepository (context);
        }

        [Fact]
        public async void TestFindAll ()
        {
            var reviews = await _reviewsRepository.FindAll ();

            Assert.NotNull (reviews);
            Assert.Single (reviews);
            Assert.Equal (1, reviews.First ().ReviewId);
        }

        [Fact]
        public async void TestAdd ()
        {
            var reviewAdded = await _reviewsRepository.Add (1, 1, 100, "Test");
            Assert.True (reviewAdded);
        }
    }
}