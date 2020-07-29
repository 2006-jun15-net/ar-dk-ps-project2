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
            var mockStudentRepo = new Mock<StudentRepository> ();

            var reviews = new List<ReviewsModel>
            {
                new ReviewsModel
                {
                    ReviewId = 1
                }
            };

            var students = new List<StudentModel>
            {
                new StudentModel
                {
                    StudentId = 1
                }
            };

            // Reviews repo setup
            mockReviewsRepo.Setup (
                repo => repo.FindAll ()
            ).Returns (
                async () => await Task.Run (() => reviews)
            );

            mockReviewsRepo.Setup (
                repo => repo.Add (It.IsAny<StudentModel> (), It.IsAny<int> (), It.IsAny<int> (), It.IsAny<string> ())
            ).Returns (
                async (StudentModel student, int courseId, int score, string text) =>
                    await Task.Run (() =>
                    {
                        reviews.Add (new ReviewsModel
                        {
                            CourseId = courseId,
                            StudentId = student.StudentId,
                            Score = score,
                            Text = text
                        });
                        return true;
                    })
            );

            // Student repo setup
            mockStudentRepo.Setup (
                repo => repo.FindById (It.IsAny<int> ())  
            ).Returns (
                async (int id) => await Task.Run (() => students.Where (s => s.StudentId == id).FirstOrDefault ())
            );

            mockReviewsRepo.SetupAllProperties ();

            _reviewsController = new ReviewsController (mockReviewsRepo.Object, mockStudentRepo.Object);
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
