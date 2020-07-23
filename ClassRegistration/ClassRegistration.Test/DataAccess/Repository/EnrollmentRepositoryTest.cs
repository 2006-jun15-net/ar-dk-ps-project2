using ClassRegistration.DataAccess.Entity;
using ClassRegistration.DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Xunit;

namespace ClassRegistration.Test.DataAccess.Repository
{
    public class EnrollmentRepositoryTest
    {
        private readonly EnrollmentRepository _enrollmentRepository;
        private readonly Course_registration_dbContext _context;

        public EnrollmentRepositoryTest ()
        {
            _context = new Course_registration_dbContext (

                new DbContextOptionsBuilder<Course_registration_dbContext> ()
                    .UseInMemoryDatabase (databaseName: "Enrollment")
                    .Options
            );

            _context.Database.EnsureDeleted ();

            _context.Add (new Enrollment
            {
                EnrollmentId = 1,
                StudentId = 1,
                SectId = 1,

                Sect = new Section
                {
                    SectId = 1,
                    CourseId = 1,
                    Term = "Test Term",

                    Course = new Course
                    {
                        CourseName = "Test Course",
                        CourseId = 1,
                        Credits = 2,
                        Fees = 1.5m
                    }
                }
            });

            _context.SaveChanges ();

            _enrollmentRepository = new EnrollmentRepository (_context);
        }

        [Fact]
        public async void TestGetCredits ()
        {
            var credits = await _enrollmentRepository.GetCredits (1, "Test Term");

            Assert.NotNull (credits);
            Assert.Equal (2, credits);
        }

        [Fact]
        public async void TestGetCreditsFail ()
        {
            var credits = await _enrollmentRepository.GetCredits (2, "Test Term");
            Assert.Null (credits);
        }

        [Fact]
        public async void TestGetCreditsFailByTermName ()
        {
            var credits = await _enrollmentRepository.GetCredits (1, "Not a term");
            Assert.Null (credits);
        }

        [Fact]
        public async void TestGetTotalAmount ()
        {
            var fees = await _enrollmentRepository.GetTotalAmount (1, "Test Term");

            Assert.NotNull (fees);
            Assert.Equal (1.5m, fees);
        }

        [Fact]
        public async void GetTotalAmountFail ()
        {
            var fees = await _enrollmentRepository.GetTotalAmount (2, "Test Term");
            Assert.Null (fees);
        }

        [Fact]
        public async void GetTotalAmountFailByTermName ()
        {
            var fees = await _enrollmentRepository.GetTotalAmount (1, "Not a term");
            Assert.Null (fees);
        }

        [Fact]
        public async void TestDelete ()
        {
            var result = await _enrollmentRepository.Delete (1, 1);
            Assert.True (result);
        }

        [Fact]
        public async void TestDeleteFail ()
        {
            var result = await _enrollmentRepository.Delete (2, 1);
            Assert.False (result);
        }

        [Fact]
        public async void TestDeleteFailByStudentId ()
        {
            var result = await _enrollmentRepository.Delete (1, 2);
            Assert.False (result);
        }

        [Fact]
        public async void TestAdd ()
        {
            await _enrollmentRepository.Add (1, 1);
            Assert.Equal (2, _context.Enrollment.Count ());
        }
    }
}
