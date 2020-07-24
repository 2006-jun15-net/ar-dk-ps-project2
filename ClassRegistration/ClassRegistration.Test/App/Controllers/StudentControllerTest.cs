using ClassRegistration.App.Controllers;
using ClassRegistration.DataAccess.Repository;
using ClassRegistration.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ClassRegistration.Test.Controllers.App
{
    public class StudentControllerTest
    {
        private readonly StudentController _studentController;

        public StudentControllerTest ()
        {
            var mockCoursesRepo = new Mock<CourseRepository> ();
            var mockStudentRepo = new Mock<StudentRepository> ();
            var mockEnrollRepo = new Mock<EnrollmentRepository> ();

            List<CourseModel> courseModels = new List<CourseModel> {

                new CourseModel {

                    StudentId = 1,
                    CourseName = "Test 1"
                }
            };

            List<StudentModel> students = new List<StudentModel> {

                new StudentModel {

                    StudentId = 1,
                    Name = "Test 1"
                },

                new StudentModel {

                    StudentId = 2,
                    Name = "Test 2"
                }
            };

            // Course repo setup
            mockCoursesRepo.Setup (
                repo => repo.FindByStudent (It.IsAny<int> ())
            ).Returns (
                async (int studentId) =>
                    await Task.Run (() => courseModels.Where (c => c.StudentId == studentId))
            );

            // Student repo setup
            mockStudentRepo.Setup (
                repo => repo.FindById (It.IsAny<int> ())
            ).Returns (
                async (int id) =>
                    await Task.Run (() => students.Where (s => s.StudentId == id).FirstOrDefault ())
            );

            // Enrollment repo setup
            // TODO 

            mockCoursesRepo.SetupAllProperties ();
            mockStudentRepo.SetupAllProperties ();
            mockEnrollRepo.SetupAllProperties ();

            _studentController = new StudentController (mockCoursesRepo.Object, mockStudentRepo.Object, mockEnrollRepo.Object);
        }

        [Fact]
        public async void TestGet ()
        {
            OkObjectResult response = await _studentController.Get (1) as OkObjectResult;

            Assert.NotNull (response);
            Assert.Equal (200, response.StatusCode);

            var student = response.Value as StudentModel;

            Assert.Equal (1, student.StudentId);
            Assert.Equal ("Test 1", student.Name);
        }

        [Fact]
        public async void TestGetFail ()
        {
            NotFoundResult response = await _studentController.Get (3) as NotFoundResult;

            Assert.NotNull (response);
            Assert.Equal (404, response.StatusCode);
        }

        [Fact]
        public async void TestGetCourses ()
        {
            OkObjectResult response = await _studentController.GetCourses (1) as OkObjectResult;

            Assert.NotNull (response);
            Assert.Equal (200, response.StatusCode);

            var courses = response.Value as IEnumerable<CourseModel>;

            Assert.Single (courses);
        }

        [Fact]
        public async void TestGetCoursesFail ()
        {
            NotFoundResult response = await _studentController.GetCourses (3) as NotFoundResult;

            Assert.NotNull (response);
            Assert.Equal (404, response.StatusCode);
        }


        [Fact]
        public async void TestGetCoursesEmpty ()
        {
            NoContentResult response = await _studentController.GetCourses (2) as NoContentResult;

            Assert.NotNull (response);
            Assert.Equal (204, response.StatusCode);
        }
    }
}
