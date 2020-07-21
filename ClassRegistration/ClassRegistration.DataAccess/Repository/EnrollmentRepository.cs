using ClassRegistration.DataAccess.Entity;
using ClassRegistration.DataAccess.Interfaces;
using ClassRegistration.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ClassRegistration.DataAccess.Repository
{
    public class EnrollmentRepository : Repository<Enrollment, EnrollmentModel>, IEnrollmentRepository
    {
        /// <summary>
        /// A constructor that intializes the database entity.
        /// </summary>
        /// <param name="context"></param>
        public EnrollmentRepository (Course_registration_dbContext context) : base (context) { }

        public EnrollmentRepository () : this (null) { }

        /// <summary>
        /// This method returns the total credits of a student with a specified id and semester
        /// </summary>
        /// <param name="id"></param>
        /// <param name="term"></param>
        /// <returns></returns>
        public virtual async Task<int?> GetCredits (int id, string term)
        {
            var totalCredits = await (from c in _context.Course
                                      join s in _context.Section on c.CourseId
                                      equals s.CourseId
                                      join e in _context.Enrollment on s.SectId equals e.SectId
                                      where e.StudentId == id && s.Term == term   // enrollments of a particular student with their respective semester.
                                      select c.Credits).ToListAsync ();

            return totalCredits.Sum ();
        }


        public virtual async Task<bool> Delete (int enrollmentId, int studentId)
        {
            var enrollment = await (from e in _context.Enrollment
                                    where e.EnrollmentId == enrollmentId && e.StudentId == studentId
                                    select e).FirstOrDefaultAsync ();

            if (enrollment == default)
            {
                return false;
            }

            _context.Enrollment.Remove (enrollment);
            await _context.SaveChangesAsync ();

            return true;
        }

        public async Task<bool> Add (EnrollmentModel enrollmentModel)
        {
            var enrollments = await (from e in _context.Enrollment
                                     where e.EnrollmentId == enrollmentModel.Id
                                     select e).ToListAsync ();

            if (enrollments.Count != 0)
            {
                return false;
            }

            _context.Enrollment.Add (new Enrollment
            {
                StudentId = enrollmentModel.StudentId,
                SectId = enrollmentModel.SectionId
            });

            await _context.SaveChangesAsync ();

            return true;
        }
    }
}
