using ClassRegistration.DataAccess.Entity;
using ClassRegistration.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ClassRegistration.DataAccess.Repository {

    public class EnrollmentRepository : Repository, IEnrollmentRepository {

        public EnrollmentRepository (Course_registration_dbContext context) : base (context) { }

        public virtual async Task<bool> Delete (int enrollmentId, int studentId) {

            var enrollment = await (from e in _context.Enrollment
                                    where e.EnrollmentId == enrollmentId && e.StudentId == studentId
                                    select e).FirstOrDefaultAsync ();

            if (enrollment == default) {
                return false;
            }

            _context.Enrollment.Remove (enrollment);
            await _context.SaveChangesAsync ();

            return true;
        }
    }
}
