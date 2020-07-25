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
    public class SectionControllerTest
    {
        private readonly SectionController _sectionController;

        public SectionControllerTest ()
        {
            var mockSectionRepo = new Mock<SectionRepository> ();

            List<SectionModel> sections = new List<SectionModel>
            {
                new SectionModel
                {
                    SectId = 1,
                    InstructorId = 1
                }
            };

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

            mockSectionRepo.SetupAllProperties ();

            _sectionController = new SectionController (mockSectionRepo.Object);
        }

        [Fact]
        public async void TestGetAll ()
        {
            OkObjectResult response = await _sectionController.Get () as OkObjectResult;

            Assert.NotNull (response);
            Assert.Equal (200, response.StatusCode);

            var sections = response.Value as IEnumerable<SectionModel>;

            Assert.Single (sections);
            Assert.Equal (1, sections.First ().SectId);
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
    }
}
