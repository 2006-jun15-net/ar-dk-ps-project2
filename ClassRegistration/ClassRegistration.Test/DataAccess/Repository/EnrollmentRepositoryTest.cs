using ClassRegistration.DataAccess.Entity;
using ClassRegistration.DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ClassRegistration.Test.DataAccess.Repository
{
    public class EnrollmentRepositoryTest
    {
        private readonly EnrollmentRepository _enrollmentRepository;

        public EnrollmentRepositoryTest ()
        {
            var context = new Course_registration_dbContext (

                new DbContextOptionsBuilder<Course_registration_dbContext> ()
                    .UseInMemoryDatabase (databaseName: "Enrollment")
                    .Options
            );

            context.Database.EnsureDeleted ();

            context.Add (new Enrollment
            {

            });

            context.SaveChanges ();

            _enrollmentRepository = new EnrollmentRepository (context);
        }

        [Fact]
        public void TestGetCredits ()
        {

        }

        [Fact]
        public void TestGetCreditsFail ()
        {

        }

        [Fact]
        public void TestGetTotalAmount ()
        {

        }

        [Fact]
        public void GetTotalAmountFail ()
        {

        }

        [Fact]
        public void TestDelete ()
        {

        }

        [Fact]
        public void TestDeleteFail ()
        {

        }

        [Fact]
        public void TestAdd ()
        {

        }
    }
}
