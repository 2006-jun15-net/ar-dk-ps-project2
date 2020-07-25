using ClassRegistration.DataAccess.Entity;
using ClassRegistration.DataAccess.Repository;
using ClassRegistration.DataAccess.Pagination;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ClassRegistration.Test.DataAccess.Repository
{
    public class CourseRepositoryTest
    {
        private readonly CourseRepository _courseRepository;

        public CourseRepositoryTest ()
        {
            var context = new Course_registration_dbContext (

                new DbContextOptionsBuilder<Course_registration_dbContext> ()
                    .UseInMemoryDatabase (databaseName: "Course")
                    .Options
            );

            context.Database.EnsureDeleted ();

            context.Add (new Course
            {
                CourseId = 1,
                CourseName = "Test",

                Credits = 1,
                Fees = 1.0m,

                DeptId = 1,
                Dept = new Department
                {
                    DeptId = 1,
                    DeptName = "Test Dept"
                },

                Section = new List<Section>
                {
                    new Section
                    {
                        SectId = 1,
                        CourseId = 1,

                        Enrollment = new List<Enrollment>
                        {
                            new Enrollment
                            {
                                EnrollmentId = 1,
                                SectId = 1,
                                StudentId = 1
                            }
                        }
                    }
                }
            });

            context.SaveChanges ();

            _courseRepository = new CourseRepository (context);
        }

        [Fact]
        public async void TestFindAll ()
        {
            var courses = await _courseRepository.FindAll (new CoursePagination ());

            Assert.Single (courses);
            Assert.Equal (1, courses.First ().CourseId);
            Assert.Equal ("Test", courses.First ().CourseName);
        }

        [Fact]
        public async void TestFindById ()
        {
            var course = await _courseRepository.FindById (1);
            Assert.Equal (1, course.CourseId);
        }

        [Fact]
        public async void TestFindByIdFail ()
        {
            var course = await _courseRepository.FindById (2);
            Assert.Null (course);
        }

        [Fact]
        public async void TestFindByName ()
        {
            var course = await _courseRepository.FindByName ("Test");
            Assert.Equal ("Test", course.CourseName);
        }

        [Fact]
        public async void TestFindByNameFail ()
        {
            var course = await _courseRepository.FindByName ("Not a course");
            Assert.Null (course);
        }

        [Fact]
        public async void TestFindByStudent ()
        {
            var courses = await _courseRepository.FindByStudent (1);

            Assert.Single (courses);
            Assert.Equal (1, courses.First ().StudentId);
        }

        [Fact]
        public async void TestFindByStudentFail ()
        {
            var courses = await _courseRepository.FindByStudent (2);
            Assert.Empty (courses);
        }

        [Fact]
        public async void TestFindByDeptId ()
        {
            var courses = await _courseRepository.FindByDeptId (1);

            Assert.Single (courses);
            Assert.Equal (1, courses.First ().DeptId);
        }

        [Fact]
        public async void TestFindByDeptIdFail ()
        {
            var courses = await _courseRepository.FindByDeptId (2);
            Assert.Empty (courses);
        }

        [Fact]
        public async void TestFindByDeptName ()
        {
            var courses = await _courseRepository.FindByDeptName ("Test Dept");

            Assert.Single (courses);
            Assert.Equal (1, courses.First ().DeptId);
        }

        [Fact]
        public async void TestFindByDeptNameFail ()
        {
            var courses = await _courseRepository.FindByDeptName ("Not a dept");
            Assert.Empty (courses);
        }
    }
}
