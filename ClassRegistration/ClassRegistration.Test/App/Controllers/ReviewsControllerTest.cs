using ClassRegistration.App.Controllers;
using ClassRegistration.DataAccess.Repository;
using ClassRegistration.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ClassRegistration.Test.App.Controllers
{
    public class ReviewsControllerTest
    {
        private readonly ReviewsController _reviewsController;

        public ReviewsControllerTest ()
        {
            var mockReviewsRepo = new Mock<ReviewsRepository> ();

            var reviews = new List<ReviewsModel>
            {
                new ReviewsModel
                {
                    ReviewId = 1
                }
            };

            mockReviewsRepo.Setup (
                repo => repo.FindAll ()
            ).Returns (
                async () => await Task.Run (() => reviews)
            );

            mockReviewsRepo.Setup (
                repo => repo.Add (It.IsAny<int> (), It.IsAny<int> (), It.IsAny<int> (), It.IsAny<string> ())
            ).Returns (
                async (int studentId, int courseId, int score, string text) =>
                    await Task.Run (() => reviews.Add (new ReviewsModel
                    {
                        CourseId = courseId,
                        StudentId = studentId,
                        Score = score,
                        Text = text
                    }))
            );

            mockReviewsRepo.SetupAllProperties ();

            _reviewsController = new ReviewsController (mockReviewsRepo.Object);
        }

        [Fact]
        public async void TestGet ()
        {
            OkObjectResult response = await _reviewsController.Get () as OkObjectResult;

            Assert.NotNull (response);
            Assert.Equal (200, response.StatusCode);

            var result = response.Value as IEnumerable<ReviewsModel>;

            Assert.Single (result);
            Assert.Equal (1, result.First ().ReviewId);
        }

        [Fact]
        public async void TestAdd ()
        {
            OkObjectResult response = await _reviewsController.Post (new ReviewsModel
            {
                CourseId = 1,
                StudentId = 1,
                Score = 100,
                Text = "Test Review"
            }) as OkObjectResult;

            Assert.NotNull (response);
            Assert.Equal (200, response.StatusCode);
        }
    }
}
