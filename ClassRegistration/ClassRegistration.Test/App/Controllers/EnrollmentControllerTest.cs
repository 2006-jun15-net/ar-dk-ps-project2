//using ClassRegistration.App.Controllers;
//using ClassRegistration.DataAccess.Repository;
//using ClassRegistration.Domain.Model;
//using Moq;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Xunit;

//namespace ClassRegistration.Test.App.Controllers
//{
//    public class EnrollmentControllerTest
//    {
//        private readonly EnrollmentController _enrollmentController;

//        public EnrollmentControllerTest ()
//        {
//            var mockEnrollmentRepo = new Mock<EnrollmentRepository> ();
//            var mockStudentRepo = new Mock<StudentRepository> ();

//            List<EnrollmentModel> enrollments = new List<EnrollmentModel>
//            {
//                new EnrollmentModel
//                {
//                    Id = 1,
//                    StudentId = 1
//                }
//            };

//            List<StudentModel> students = new List<StudentModel>
//            {
//                new StudentModel 
//                {
//                    Id = 1,
//                    Name = "Test 1"
//                }
//            };

//            // Enrollment repo setup
//            mockEnrollmentRepo.Setup (
//                repo => repo.GetCredits (It.IsAny<int> (), It.IsAny<string> ())
//            ).Returns (
//                async (int id, string term) => 
//                    await Task.Run (() => 1) // TODO fix
//            );

//            mockEnrollmentRepo.Setup (
//                repo => repo.Add (It.IsAny<EnrollmentModel> ())
//            ).Returns (
//                async (EnrollmentModel enrollment) =>
//                    await Task.Run (() =>
//                    {
//                        if (enrollments.Where (e => e.Id == enrollment.Id).Count () != 0)
//                        {
//                            return false;
//                        }

//                        enrollments.Add (enrollment);

//                        return true;
//                    })
//            );

//            mockEnrollmentRepo.Setup (
//                repo => repo.Delete (It.IsAny<int> (), It.IsAny<int> ())
//            ).Returns (
//                async (int id, int studentId) =>
//                    await Task.Run (() => enrollments.RemoveAll (e => e.Id == id && e.StudentId == studentId) > 0)
//            );

//            // Student repo setup
//            mockStudentRepo.Setup (
//                repo => repo.FindById (It.IsAny<int> ())
//            ).Returns (
//                async (int id) =>
//                    await Task.Run (() => students.Where (s => s.Id == id).FirstOrDefault ())
//            );

//            mockEnrollmentRepo.SetupAllProperties ();
//            mockStudentRepo.SetupAllProperties ();

//            _enrollmentController = new EnrollmentController (mockEnrollmentRepo.Object, mockStudentRepo.Object);
//        }

//        [Fact]
//        public void TestGetTotalCredits ()
//        {

//        }

//        [Fact]
//        public void TestGetTotalCreditsFail ()
//        {

//        }

//        [Fact]
//        public void TestPost ()
//        {

//        }

//        [Fact]
//        public void TestPostFail ()
//        {

//        }

//        [Fact]
//        public void TestDelete ()
//        {

//        }

//        [Fact]
//        public void TestDeleteFail ()
//        {

//        }
//    }
//}
