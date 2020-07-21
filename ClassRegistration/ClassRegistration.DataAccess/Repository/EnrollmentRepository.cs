using ClassRegistration.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassRegistration.DataAccess.Interfaces;

namespace ClassRegistration.DataAccess.Repository
{
    public class EnrollmentRepository : Repository, IEnrollmentRepository
    {
        /// <summary>
        /// A constructor that intializes the database entity.
        /// </summary>
        /// <param name="context"></param>
        public EnrollmentRepository(Course_registration_dbContext context) : base (context) { }

        public EnrollmentRepository (Course_registration_dbContext context) : this(null) { }

        /// <summary>
        /// This method returns the total credits of a student with a specified id and semester
        /// </summary>
        /// <param name="id"></param>
        /// <param name="term"></param>
        /// <returns></returns>
        public Task<int> GetCredits(int id, string term)
        {
            var totalCredits = from c in _context.Course
                               join s in _context.Section on c.CourseId
                               equals s.CourseId
                               join e in _context.Enrollment on s.SectId equals e.SectId
                               where e.StudentId == id && s.Term == term   // enrollments of a particular student with their respective semester.
                               select c.Credits;

            return await totalCredits.SumAsync ();
        }

        /// <summary>
        /// This method saves chnages to the database context.
        /// </summary>
        public Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
