using ClassRegistration.App.Controllers;
using ClassRegistration.DataAccess.Repository;
using ClassRegistration.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ClassRegistration.Test.App.Controllers
{
    public class SectionControllerTest
    {
        private readonly SectionController _sectionController;

        public SectionControllerTest ()
        {
            var mockSectionRepo = new Mock<SectionRepository> ();
            var mockCourseRepo = new Mock<CourseRepository> ();

            List<SectionModel> sections = new List<SectionModel>
            {
                new SectionModel
                {
                    SectId = 1,
                    InstructorId = 1
                }
            };

            List<CourseModel> courses = new List<CourseModel>
            {
                new CourseModel
                {
                    CourseId = 1,
                }
            };

            // Section repo setup
            mockSectionRepo.Setup (
                repo => repo.FindAll ()
            ).Returns (
                async () => await Task.Run (() => sections)
            );

            mockSectionRepo.Setup (
                repo => repo.FindByInstrId (It.IsAny<int> ())
            ).Returns (
                async (int instructorId) =>
                    await Task.Run (() => sections.Where (s => s.InstructorId == instructorId))
            );

            mockSectionRepo.Setup (
                repo => repo.Add (It.IsAny<int> (), It.IsAny<int> (), It.IsAny<string> (), It.IsAny<TimeSpan> (), It.IsAny<TimeSpan> ())
            ).Returns (
                async (int instructorId, int courseId, string term, TimeSpan start, TimeSpan end) =>
                    await Task.Run (() =>
                    {
                        sections.Add (new SectionModel
                        {
                            InstructorId = instructorId,
                            CourseId = courseId,
                            Term = term,
                            StartTime = start,
                            EndTime = end
                        });
                        return true;
                    })
           );

            // Course repo setup
            mockCourseRepo.Setup (
                repo => repo.FindById (It.IsAny<int> ())
            ).Returns (
                async (int id) =>
                    await Task.Run (() => courses.Where (c => c.CourseId == id).FirstOrDefault ())
            );

            mockSectionRepo.SetupAllProperties ();

            _sectionController = new SectionController (mockSectionRepo.Object, mockCourseRepo.Object);
        }

        [Fact]
        public async void TestGetCourseByInstructor ()
        {
            OkObjectResult response = await _sectionController.Get (1) as OkObjectResult;

            Assert.NotNull (response);
            Assert.Equal (200, response.StatusCode);

            var sections = response.Value as IEnumerable<SectionModel>;

            Assert.Single (sections);
            Assert.Equal (1, sections.First ().InstructorId);
        }

        [Fact]
        public async void TestGetCourseByInstructorFail ()
        {
            NoContentResult response = await _sectionController.Get (2) as NoContentResult;

            Assert.NotNull (response);
            Assert.Equal (204, response.StatusCode);
        }

        [Fact]
        public async void TestAdd ()
        {
            OkObjectResult response = await _sectionController.Post (new SectionModel
            {
                InstructorId = 1,
                CourseId = 1,
                Term = "fall",
                StartTime = new TimeSpan (),
                EndTime = new TimeSpan ()
            }) as OkObjectResult;

            Assert.NotNull (response);
            Assert.Equal (200, response.StatusCode);
        }

        [Fact]
        public async void TestAddFail ()
        {
            BadRequestObjectResult response = await _sectionController.Post (new SectionModel
            {
                InstructorId = 1,
                CourseId = 2,
                Term = "fall",
                StartTime = new TimeSpan (),
                EndTime = new TimeSpan ()
            }) as BadRequestObjectResult;

            Assert.NotNull (response);
            Assert.Equal (400, response.StatusCode);
        }
    }
}
