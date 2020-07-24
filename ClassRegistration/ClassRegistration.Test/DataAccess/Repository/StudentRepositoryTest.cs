using ClassRegistration.DataAccess.Entity;
using ClassRegistration.DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ClassRegistration.Test.DataAccess.Repository
{
    public class StudentRepositoryTest
    {
        private readonly StudentRepository _studentRepository;

        public StudentRepositoryTest ()
        {
            var context = new Course_registration_dbContext (

                new DbContextOptionsBuilder<Course_registration_dbContext> ()
                    .UseInMemoryDatabase (databaseName: "Student")
                    .Options
            );

            context.Database.EnsureDeleted ();

            context.Add (new Student
            {
                StudentId = 1,
                ResidentId = "in-state",

                FirstName = "Test",
                LastName = "Student"
            });

            context.SaveChanges ();

            _studentRepository = new StudentRepository (context);
        }

        [Fact]
        public async void TestFindById ()
        {
            var student = await _studentRepository.FindById (1);

            Assert.NotNull (student);
            Assert.Equal (1, student.StudentId);
        }

        [Fact]
        public async void TestFindByIdFail ()
        {
            var student = await _studentRepository.FindById (2);
            Assert.Null (student);
        }
    }
}
