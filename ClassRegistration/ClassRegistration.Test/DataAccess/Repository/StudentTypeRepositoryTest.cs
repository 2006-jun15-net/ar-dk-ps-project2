using ClassRegistration.DataAccess.Entity;
using ClassRegistration.DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ClassRegistration.Test.DataAccess.Repository
{
    public class StudentTypeRepositoryTest
    {
        private readonly StudentTypeRepository _studentTypeRepository;

        public StudentTypeRepositoryTest ()
        {
            var context = new Course_registration_dbContext (

                new DbContextOptionsBuilder<Course_registration_dbContext> ()
                    .UseInMemoryDatabase (databaseName: "StudentType")
                    .Options
            );

            context.Database.EnsureDeleted ();

            context.Add (new StudentType
            {
                ResidentId = "In-State",
                Discount = 1m
            });

            context.SaveChanges ();

            _studentTypeRepository = new StudentTypeRepository (context);
        }

        [Fact]
        public async void TestFindDiscount ()
        {
            var discount = await _studentTypeRepository.FindDiscount ("in-state");
            Assert.Equal (1m, discount);
        }
    }
}
