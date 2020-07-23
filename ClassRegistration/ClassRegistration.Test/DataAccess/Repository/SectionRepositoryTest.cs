using ClassRegistration.DataAccess.Entity;
using ClassRegistration.DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ClassRegistration.Test.DataAccess.Repository
{
    public class SectionRepositoryTest
    {
        private readonly SectionRepository _sectionRepository;

        public SectionRepositoryTest ()
        {
            var context = new Course_registration_dbContext (

                new DbContextOptionsBuilder<Course_registration_dbContext> ()
                    .UseInMemoryDatabase (databaseName: "Section")
                    .Options
            );

            context.Database.EnsureDeleted ();

            context.Add (new Section
            {
                SectId = 1,
                InstructorId = 1,

                Course = new Course
                {
                    Reviews = new List<Reviews> ()
                }
            });

            context.SaveChanges ();

            _sectionRepository = new SectionRepository (context);
        }

        [Fact]
        public async void TestFindAll ()
        {
            var sections = await _sectionRepository.FindAll ();

            Assert.NotNull (sections);
            Assert.Single (sections);
            Assert.Equal (1, sections.First ().SectId);
        }

        [Fact]
        public async void TestFindById ()
        {
            var sectionById = await _sectionRepository.FindById (1);

            Assert.NotNull (sectionById);
            Assert.Equal (1, sectionById.SectId);
        }

        [Fact]
        public async void TestFindByIdFail ()
        {
            var sectionById = await _sectionRepository.FindById (2);
            Assert.Null (sectionById);
        }

        [Fact]
        public async void TestFindByInstrId ()
        {
            var sectionsByInstrId = await _sectionRepository.FindByInstrId (1);

            Assert.NotNull (sectionsByInstrId);
            Assert.Single (sectionsByInstrId);
            Assert.Equal (1, sectionsByInstrId.First ().InstructorId);
        }

        [Fact]
        public async void TestFindByInstrIdFail ()
        {
            var sectionsByInstrId = await _sectionRepository.FindByInstrId (2);
            Assert.Null (sectionsByInstrId);
        }
    }
}
