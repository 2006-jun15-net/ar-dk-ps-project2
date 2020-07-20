using ClassRegistration.DataAccess.Entity;
using ClassRegistration.Domain;
using ClassRegistration.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ClassRegistration.DataAccess.Repository {

    public class StudentRepository : Repository, IStudentRepository {

        public StudentRepository (Course_registration_dbContext context) : base (context) { }

        public StudentRepository () : this (null) { }

        public virtual async Task AddEnrollment (int studentId, EnrollmentModel enrollmentModel) {

            _context.Enrollment.Add (new Enrollment {

                StudentId = enrollmentModel.StudentId,
                SectId = enrollmentModel.SectionId
            });

            await _context.SaveChangesAsync ();
        }

        public virtual async Task<bool> DeleteEnrollment (int studentId, int enrollmentId) {

            var student = await (from s in _context.Student
                                 where s.StudentId == studentId
                                 select s).FirstOrDefaultAsync ();

            if (student == default) {
                return false;
            }

            var enrollment = (from e in student.Enrollment
                              where e.EnrollmentId == enrollmentId
                              select e).FirstOrDefault ();

            if (enrollment == default) {
                return false;
            }

            if (!student.Enrollment.Remove (enrollment)) {
                return false;
            }

            await _context.SaveChangesAsync ();

            return true;
        }
    }
}
