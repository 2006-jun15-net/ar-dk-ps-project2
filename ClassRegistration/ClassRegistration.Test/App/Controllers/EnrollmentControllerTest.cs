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
    public class EnrollmentControllerTest
    {
        private readonly EnrollmentController _enrollmentController;

        public EnrollmentControllerTest ()
        {
            var mockEnrollmentRepo = new Mock<EnrollmentRepository> ();
            var mockStudentRepo = new Mock<StudentRepository> ();
            var mockSectionRepo = new Mock<SectionRepository> ();

            List<EnrollmentModel> enrollments = new List<EnrollmentModel>
            {
                new EnrollmentModel
                {
                    EnrollmentId = 1,
                    SectId = 1,
                    StudentId = 1,

                    Section = new SectionModel
                    {
                        SectId = 1,
                        Term = "fall",

                        Course = new CourseModel
                        {
                            CourseId = 1,
                            Credits = 20
                        }
                    }
                },

                new EnrollmentModel
                { 
                    EnrollmentId = 2,
                    SectId = 2,
                    StudentId = 2,

                    Section = new SectionModel
                    {
                        SectId = 2,
                        Term = "spring",

                        Course = new CourseModel
                        {
                            CourseId = 2,
                            Credits = 1
                        }
                    }   
                }
            };

            List<StudentModel> students = new List<StudentModel>
            {
                new StudentModel
                {
                    StudentId = 1,
                    Name = "Test Student 1"
                },

                new StudentModel
                {
                    StudentId = 2,
                    Name = "Test Student 2"
                }
            };

            List<SectionModel> sections = new List<SectionModel>
            {
                new SectionModel
                {
                    SectId = 1,
                    InstructorId = 1
                },

                new SectionModel
                {
                    SectId = 2,
                    InstructorId = 1
                }
            };

            // Enrollment repo setup
            mockEnrollmentRepo.Setup (
                repo => repo.GetCredits (It.IsAny<int> (), It.IsAny<string> ())
            ).Returns (
                async (int id, string term) =>
                    await Task.Run (() => enrollments.Where (e => e.EnrollmentId == id).Select (e => e.Section)
                                        .Where (s => s.Term == term).Select (s => s.Course.Credits).FirstOrDefault ()) 
            );

            mockEnrollmentRepo.Setup (
                repo => repo.Add (It.IsAny<int> (), It.IsAny<int> ())
            ).Returns (
                async (int studentId, int sectionId) =>
                    await Task.Run (() => enrollments.Add (new EnrollmentModel
                    {
                        StudentId = studentId,
                        SectId = sectionId
                    }))
            );

            mockEnrollmentRepo.Setup (
                repo => repo.Delete (It.IsAny<int> (), It.IsAny<int> ())
            ).Returns (
                async (int id, int studentId) =>
                    await Task.Run (() => enrollments.Where (e => e.EnrollmentId == id && e.StudentId == studentId).Any ())
            );

            // Student repo setup
            mockStudentRepo.Setup (
                repo => repo.FindById (It.IsAny<int> ())
            ).Returns (
                async (int id) =>
                    await Task.Run (() => students.Where (s => s.StudentId == id).FirstOrDefault ())
            );

            // Section repo setup
            mockSectionRepo.Setup (
                repo => repo.FindAll ()
            ).Returns (
                async () =>
                    await Task.Run (() => sections)
            );

            mockSectionRepo.Setup (
                repo => repo.FindById (It.IsAny<int> ())
            ).Returns (
                async (int id) =>
                    await Task.Run (() => sections.Where (s => s.SectId == id).FirstOrDefault ())
            );

            mockSectionRepo.Setup (
                repo => repo.FindByInstrId (It.IsAny<int> ())
            ).Returns (
                async (int instrId) =>
                    await Task.Run (() => sections.Where (s => s.InstructorId == instrId))
            );

            mockEnrollmentRepo.SetupAllProperties ();
            mockStudentRepo.SetupAllProperties ();
            mockSectionRepo.SetupAllProperties ();

            _enrollmentController = new EnrollmentController (mockEnrollmentRepo.Object, mockStudentRepo.Object, mockSectionRepo.Object);
        }

        [Fact]
        public async void TestGetTotalCredits ()
        {
            OkObjectResult response = await _enrollmentController.GetTotalCredits (1, "fall") as OkObjectResult;

            Assert.NotNull (response);
            Assert.Equal (200, response.StatusCode);

            bool result = (bool)response.Value;

            Assert.True (result);
        }

        [Fact]
        public async void TestGetTotalCreditsTermReqNotMet ()
        {
            OkObjectResult response = await _enrollmentController.GetTotalCredits (2, "spring") as OkObjectResult;

            Assert.NotNull (response);
            Assert.Equal (200, response.StatusCode);

            bool result = (bool)response.Value;

            Assert.False (result);
        }

        [Fact]
        public async void TestGetTotalCreditsFailById ()
        {
            BadRequestResult response = await _enrollmentController.GetTotalCredits (3, "fall") as BadRequestResult;

            Assert.NotNull (response);
            Assert.Equal (400, response.StatusCode);
        }

        [Fact]
        public async void TestGetTotalCreditsFailByTerm ()
        {
            BadRequestResult response = await _enrollmentController.GetTotalCredits (1, "Not a term") as BadRequestResult;

            Assert.NotNull (response);
            Assert.Equal (400, response.StatusCode);
        }

        [Fact]
        public async void TestPost ()
        {
            OkResult response = await _enrollmentController.Post (new EnrollmentModel
            {
                StudentId = 2,
                SectId = 2
            }) as OkResult;

            Assert.NotNull (response);
            Assert.Equal (200, response.StatusCode);
        }

        [Fact]
        public async void TestPostFailByStudentId ()
        {
            BadRequestResult response = await _enrollmentController.Post (new EnrollmentModel
            {
                StudentId = 3,
                SectId = 2
            }) as BadRequestResult;

            Assert.NotNull (response);
            Assert.Equal (400, response.StatusCode);
        }

        [Fact]
        public async void TestPostFailBySectionId ()
        {
            BadRequestResult response = await _enrollmentController.Post (new EnrollmentModel
            {
                StudentId = 2,
                SectId = 3
            }) as BadRequestResult;

            Assert.NotNull (response);
            Assert.Equal (400, response.StatusCode);
        }

        [Fact]
        public async void TestDelete ()
        {
            OkResult response = await _enrollmentController.Delete (1, 1) as OkResult;

            Assert.NotNull (response);
            Assert.Equal (200, response.StatusCode);
        }

        [Fact]
        public async void TestDeleteFailById ()
        {
            NotFoundResult response = await _enrollmentController.Delete (3, 1) as NotFoundResult;

            Assert.NotNull (response);
            Assert.Equal (404, response.StatusCode);
        }

        [Fact]
        public async void TestDeleteFailByStudentId ()
        {
            BadRequestResult response = await _enrollmentController.Delete (1, 3) as BadRequestResult;

            Assert.NotNull (response);
            Assert.Equal (400, response.StatusCode);
        }
    }
}
