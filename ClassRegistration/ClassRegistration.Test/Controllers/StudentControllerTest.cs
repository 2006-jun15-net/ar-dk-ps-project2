using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using ClassRegistration.App.Controllers;
using ClassRegistration.DataAccess.Entity;
using ClassRegistration.DataAccess.Repository;
using ClassRegistration.Domain.Model;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ClassRegistration.Test.Controllers {

    public class StudentControllerTest {

        private readonly StudentController _studentController;

        public StudentControllerTest () {

            var mockCoursesRepo = new Mock<CourseRepository> ();
            var mockStudentRepo = new Mock<StudentRepository> ();

            List<CourseModel> courseModels = new List<CourseModel> {

                new CourseModel {

                    StudentId = 1,
                    CourseName = "Test 1"
                }
            };

            List<StudentModel> students = new List<StudentModel> {

                new StudentModel {

                    Id = 1,
                    Name = "Test Student"
                }
            };
            
            mockCoursesRepo.Setup (
                repo => repo.FindByStudent (It.IsAny<int> ())
            ).Returns (
                async (int studentId) => 
                    await Task.Run ( () => courseModels.Where (c => c.StudentId == studentId) )
            );

            mockStudentRepo.Setup (
                repo => repo.FindById (It.IsAny<int> ())
            ).Returns (
                async (int id) => 
                    await Task.Run ( () => students.Where (s => s.Id == id).FirstOrDefault () )
            );

            mockCoursesRepo.SetupAllProperties ();
            mockStudentRepo.SetupAllProperties ();

            _studentController = new StudentController (mockCoursesRepo.Object, mockStudentRepo.Object);
        }

        [Fact]
        public void TestGet () {

            var student = _studentController.Get (1);
        }

        [Fact]
        public void TestGetCourses () {

            var coursesForStudent = _studentController.GetCourses (1);
        }
    }
}
