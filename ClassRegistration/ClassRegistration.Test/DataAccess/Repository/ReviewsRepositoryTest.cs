using ClassRegistration.DataAccess.Entity;
using ClassRegistration.DataAccess.Repository;
using ClassRegistration.Domain.Model;
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
                ReviewId = 1,
                Text = "Test Review"
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
            await _reviewsRepository.Add (new StudentModel
            {

            }, 1, 100, "Test");
        }
    }
}
