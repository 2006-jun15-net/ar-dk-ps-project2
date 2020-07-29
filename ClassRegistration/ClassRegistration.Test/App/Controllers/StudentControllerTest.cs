using ClassRegistration.App.Controllers;
using ClassRegistration.DataAccess.Repository;
using ClassRegistration.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
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
            var mockStudentRepo = new Mock<StudentRepository> ();
            var mockEnrollRepo = new Mock<EnrollmentRepository> ();
            var mockStudentTypeRepo = new Mock<StudentTypeRepository> ();

            List<StudentModel> students = new List<StudentModel> {

                new StudentModel {

                    StudentId = 1,
                    FirstName = "Test",
                    LastName = "1",
                    ResidentId = "in-state"
                },

                new StudentModel {

                    StudentId = 2,
                    FirstName = "Test",
                    LastName = "2",
                    ResidentId = "out-of-state"
                }
            };

            List<EnrollmentModel> enrollments = new List<EnrollmentModel>
            {
                new EnrollmentModel
                {
                    EnrollmentId = 1,
                    StudentId = 1,

                    Section = new SectionModel
                    {
                        Term = "fall",

                        Course = new CourseModel
                        {
                            CourseName = "Test 1",

                            Fees = 1.0m,
                            Credits = 1
                        }
                    }
                }
            };

            // Student repo setup
            mockStudentRepo.Setup (
                repo => repo.FindById (It.IsAny<int> ())
            ).Returns (
                async (int id) =>
                    await Task.Run (() => students.Where (s => s.StudentId == id).FirstOrDefault ())
            );

            // Enrollment repo setup
            mockEnrollRepo.Setup (
                repo => repo.FindByStudent (It.IsAny<int> ())
            ).Returns (
                async (int id) => 
                    await Task.Run (() => enrollments.Where (e => e.StudentId == id))
            );

            mockEnrollRepo.Setup (
                repo => repo.GetTotalAmount (It.IsAny<int> (), It.IsAny<string> ())  
            ).Returns (
                async (int id, string term) =>
                    await Task.Run (() => {

                        var courses = enrollments.Where (e => e.EnrollmentId == id).Select (e => e.Section)
                                                .Where (s => s.Term == term).Select (s => s.Course);

                        if (!courses.Any ())
                        {
                            return null;
                        }

                        return (decimal?)courses.Select (c => c.Fees).Sum ();
                    })
            );

            // StudentType repo setup
            mockStudentTypeRepo.Setup (
                repo => repo.FindDiscount (It.IsAny<string> ())
            ).Returns (
                async (string id) => 
                    await Task.Run (() => id == "in-state" ? 1m : 0m)
            );
            
            mockStudentRepo.SetupAllProperties ();
            mockEnrollRepo.SetupAllProperties ();
            mockStudentTypeRepo.SetupAllProperties ();

            _studentController = new StudentController (mockStudentRepo.Object, mockEnrollRepo.Object, mockStudentTypeRepo.Object);
        }

        [Fact]
        public async void TestGet ()
        {
            OkObjectResult response = await _studentController.Get (1) as OkObjectResult;

            Assert.NotNull (response);
            Assert.Equal (200, response.StatusCode);

            var student = response.Value as StudentModel;

            Assert.Equal (1, student.StudentId);
            Assert.Equal ("Test", student.FirstName);
            Assert.Equal ("1", student.LastName);
        }

        [Fact]
        public async void TestGetFail ()
        {
            NotFoundObjectResult response = await _studentController.Get (3) as NotFoundObjectResult;

            Assert.NotNull (response);
            Assert.Equal (404, response.StatusCode);
        }

        [Fact]
        public async void TestGetCourses ()
        {
            OkObjectResult response = await _studentController.GetCourses (1) as OkObjectResult;

            Assert.NotNull (response);
            Assert.Equal (200, response.StatusCode);

            var courses = response.Value as IEnumerable<EnrollmentModel>;

            Assert.Single (courses);
        }

        [Fact]
        public async void TestGetCoursesFail ()
        {
            NotFoundObjectResult response = await _studentController.GetCourses (3) as NotFoundObjectResult;

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

        [Fact]
        public async void TestGetTotalAmount ()
        {
            OkObjectResult response = await _studentController.GetTotalAmount (1, "fall") as OkObjectResult;

            Assert.NotNull (response);
            Assert.Equal (200, response.StatusCode);

            var amount = (decimal)response.Value;

            Assert.Equal (1m, amount);
        }

        [Fact]
        public async void TestGetTotalAmountFailById ()
        { 
            NotFoundObjectResult response = await _studentController.GetTotalAmount (5, "fall") as NotFoundObjectResult;

            Assert.NotNull (response);
            Assert.Equal (404, response.StatusCode);
        }

        [Fact]
        public async void TestGetTotalAmountFailByTerm ()
        {
            BadRequestObjectResult response = await _studentController.GetTotalAmount (1, "Not a term") as BadRequestObjectResult;

            Assert.NotNull (response);
            Assert.Equal (400, response.StatusCode);
        }

        [Fact]
        public async void TestGetDiscount ()
        {
            OkObjectResult response = await _studentController.GetDiscount (1) as OkObjectResult;

            Assert.NotNull (response);
            Assert.Equal (200, response.StatusCode);

            var discount = (decimal)response.Value;

            Assert.Equal (1m, discount);
        }

        [Fact]
        public async void TestGetDiscountFail ()
        {
            NotFoundObjectResult response = await _studentController.GetDiscount (5) as NotFoundObjectResult;

            Assert.NotNull (response);
            Assert.Equal (404, response.StatusCode);
        }
    }
}
