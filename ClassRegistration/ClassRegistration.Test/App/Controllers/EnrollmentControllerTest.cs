using ClassRegistration.App.Controllers;
using ClassRegistration.DataAccess.Repository;
using ClassRegistration.Domain.Model;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ClassRegistration.Test.App.Controllers
{
    public class EnrollmentControllerTest
    {
        private readonly EnrollmentController _enrollmentController;

        public EnrollmentControllerTest ()
        {
            var mockEnrollmentRepo = new Mock<EnrollmentRepository> ();
            var mockStudentRepo = new Mock<StudentRepository> ();
            var mockSectionRepo = new Mock<SectionRepository> ();

            List<EnrollmentModel> enrollments = new List<EnrollmentModel>
            {
                new EnrollmentModel
                {
                    EnrollmentId = 1,
                    SectId = 1,
                    StudentId = 1
                }
            };

            List<StudentModel> students = new List<StudentModel>
            {
                new StudentModel
                {
                    StudentId = 1,
                    Name = "Test 1"
                }
            };

            List<SectionModel> sections = new List<SectionModel>
            {
                new SectionModel
                {
                    SectId = 1,
                    InstructorId = 1
                }
            };

            // Enrollment repo setup
            mockEnrollmentRepo.Setup (
                repo => repo.GetCredits (It.IsAny<int> (), It.IsAny<string> ())
            ).Returns (
                async (int id, string term) =>
                    await Task.Run (() => 1) // TODO fix
            );

            mockEnrollmentRepo.Setup (
                repo => repo.Add (It.IsAny<EnrollmentModel> ())
            ).Returns (
                async (EnrollmentModel enrollment) =>
                    await Task.Run (() =>
                    {
                        if (enrollments.Where (e => e.EnrollmentId == enrollment.EnrollmentId).Count () != 0)
                        {
                            return false;
                        }

                        enrollments.Add (enrollment);

                        return true;
                    })
            );

            mockEnrollmentRepo.Setup (
                repo => repo.Delete (It.IsAny<int> (), It.IsAny<int> ())
            ).Returns (
                async (int id, int studentId) =>
                    await Task.Run (() => enrollments.RemoveAll (e => e.EnrollmentId == id && e.StudentId == studentId) > 0)
            );

            // Student repo setup
            mockStudentRepo.Setup (
                repo => repo.FindById (It.IsAny<int> ())
            ).Returns (
                async (int id) =>
                    await Task.Run (() => students.Where (s => s.StudentId == id).FirstOrDefault ())
            );

            // Section repo setup
            mockSectionRepo.Setup (
                repo => repo.FindAll ()
            ).Returns (
                async () =>
                    await Task.Run (() => sections)
            );

            mockSectionRepo.Setup (
                repo => repo.FindById (It.IsAny<int> ())
            ).Returns (
                async (int id) =>
                    await Task.Run (() => sections.Where (s => s.SectId == id).FirstOrDefault ())
            );

            mockSectionRepo.Setup (
                repo => repo.FindByInstrId (It.IsAny<int> ())
            ).Returns (
                async (int instrId) =>
                    await Task.Run (() => sections.Where (s => s.InstructorId == instrId))
            );

            mockEnrollmentRepo.SetupAllProperties ();
            mockStudentRepo.SetupAllProperties ();
            mockSectionRepo.SetupAllProperties ();

            _enrollmentController = new EnrollmentController (mockEnrollmentRepo.Object, mockStudentRepo.Object, mockSectionRepo.Object);
        }

        [Fact]
        public void TestGetTotalCredits ()
        {

        }

        [Fact]
        public void TestGetTotalCreditsFail ()
        {

        }

        [Fact]
        public void TestPost ()
        {

        }

        [Fact]
        public void TestPostFail ()
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
    }
}
