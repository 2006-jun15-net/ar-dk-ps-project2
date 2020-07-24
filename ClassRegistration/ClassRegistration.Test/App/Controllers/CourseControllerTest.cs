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
    public class CourseControllerTest
    {
        private readonly CourseController _courseController;

        public CourseControllerTest ()
        {
            var mockCourseRepo = new Mock<CourseRepository> ();

            var courses = new List<CourseModel>
            {
                new CourseModel
                {
                    CourseId = 1,
                    CourseName = "Test",
                    DeptId = 1
                }
            };

            mockCourseRepo.Setup (
                repo => repo.FindAll ()
            ).Returns (
                async () => await Task.Run (() => courses)
            );

            mockCourseRepo.Setup (
                repo => repo.FindById (It.IsAny<int> ())
            ).Returns (
                async (int id) =>
                    await Task.Run (() => courses.Where (c => c.CourseId == id).FirstOrDefault ())
            );

            mockCourseRepo.Setup (
                repo => repo.FindByName (It.IsAny<string> ())
            ).Returns (
                async (string name) =>
                    await Task.Run (() => courses.Where (c => c.CourseName == name).FirstOrDefault ())
            );

            mockCourseRepo.Setup (
                repo => repo.FindByDeptId (It.IsAny<int> ())
            ).Returns (
                async (int deptId) =>
                    await Task.Run (() => courses.Where (c => c.DeptId == deptId))
            );

            mockCourseRepo.SetupAllProperties ();

            _courseController = new CourseController (mockCourseRepo.Object);
        }

        [Fact]
        public async void TestGet ()
        {
            OkObjectResult response = await _courseController.Get () as OkObjectResult;

            Assert.NotNull (response);
            Assert.Equal (200, response.StatusCode);

            var courses = response.Value as IEnumerable<CourseModel>;

            Assert.Single (courses);
            Assert.Equal (1, courses.First ().CourseId);
        }

        [Fact]
        public async void TestGetById ()
        {
            OkObjectResult response = await _courseController.Get (1) as OkObjectResult;

            Assert.NotNull (response);
            Assert.Equal (200, response.StatusCode);

            var course = response.Value as CourseModel;

            Assert.Equal (1, course.CourseId);
        }

        [Fact]
        public async void TestGetByIdFail ()
        {
            NotFoundObjectResult response = await _courseController.Get (2) as NotFoundObjectResult;

            Assert.NotNull (response);
            Assert.Equal (404, response.StatusCode);
        }

        [Fact]
        public async void TestGetByName ()
        {
            OkObjectResult response = await _courseController.GetByName ("Test") as OkObjectResult;

            Assert.NotNull (response);
            Assert.Equal (200, response.StatusCode);

            var course = response.Value as CourseModel;

            Assert.Equal ("Test", course.CourseName);
        }

        [Fact]
        public async void TestGetByNameFail ()
        {
            NotFoundObjectResult response = await _courseController.GetByName ("Not a course") as NotFoundObjectResult;

            Assert.NotNull (response);
            Assert.Equal (404, response.StatusCode);
        }

        [Fact]
        public async void TestGetByDepartmentId ()
        {
            OkObjectResult response = await _courseController.GetByDepartmentId (1) as OkObjectResult;

            Assert.NotNull (response);
            Assert.Equal (200, response.StatusCode);

            var courses = response.Value as IEnumerable<CourseModel>;

            Assert.Single (courses);
            Assert.Equal (1, courses.First ().DeptId);
        }

        [Fact]
        public async void TestGetByDepartmentIdFail ()
        {
            NotFoundObjectResult response = await _courseController.GetByDepartmentId (2) as NotFoundObjectResult;

            Assert.NotNull (response);
            Assert.Equal (404, response.StatusCode);
        }
    }
}
