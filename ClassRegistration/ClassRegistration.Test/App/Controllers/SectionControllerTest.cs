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
                    InstructorId = 1,
                    CourseId = 1
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
                repo => repo.FindByCourseId (It.IsAny<int> ())
            ).Returns (
                async (int courseId) =>
                    await Task.Run (() => sections.FirstOrDefault (s => s.CourseId == courseId))
            );

            mockSectionRepo.SetupAllProperties ();

            _sectionController = new SectionController (mockSectionRepo.Object);
        }

        [Fact]
        public async void TestGetByCourseId ()
        {
            OkObjectResult response = await _sectionController.Get (1) as OkObjectResult;

            Assert.NotNull (response);
            Assert.Equal (200, response.StatusCode);

            var section = response.Value as SectionModel;

            Assert.Equal (1, section.CourseId);
        }
    }
}
