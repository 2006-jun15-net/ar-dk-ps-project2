using ClassRegistration.DataAccess.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using ClassRegistration.Domain;
using ClassRegistration.Domain.Model;
using System.Collections.Generic;

namespace ClassRegistration.DataAccess.Repository {

    public class CourseRepository : Repository, ICourseRepository {

        public CourseRepository (Course_registration_dbContext context) : base (context) { }

        public virtual async Task<IEnumerable<CourseModel>> FindByStudent (int studentId) {

            var enrollments = _context.Enrollment.Where (e => e.StudentId == studentId);

            return await enrollments.Include (e => e.Sect).Select (e => e.Sect)
                                .Include (s => s.Course).Select (s => new CourseModel {
                                    CourseName = s.Course.CourseName
                                }).ToListAsync ();
        }
    }
}
