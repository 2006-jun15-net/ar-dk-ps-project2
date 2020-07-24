using ClassRegistration.DataAccess.Entity;
using ClassRegistration.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ClassRegistration.DataAccess.Repository
{
    public class EnrollmentRepository : Repository, IEnrollmentRepository
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
            var courses = await (from c in _context.Course
                                 join s in _context.Section on c.CourseId equals s.CourseId
                                 join e in _context.Enrollment on s.SectId equals e.SectId
                                 where e.StudentId == id && s.Term == term   // enrollments of a particular student with their respective semester.
                                 select c).ToListAsync ();

            if (!courses.Any ())
            {
                return null;
            }

            return courses.Select (c => c.Credits).Sum ();
        }

        /// <summary>
        /// This methods gets the total amount of registered courses of a student in a specified term.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="term"></param>
        /// <returns></returns>
        public virtual async Task<decimal?> GetTotalAmount (int id, string term)
        {
            var courses = await (from c in _context.Course
                                 join s in _context.Section on c.CourseId equals s.CourseId
                                 join e in _context.Enrollment on s.SectId equals e.SectId
                                 where e.StudentId == id && s.Term == term   // enrollments of a particular student with their respective semester.
                                 select c).ToListAsync ();

            if (!courses.Any ())
            {
                return null;
            }

            return courses.Select (c => c.Fees).Sum ();
        }
        
        /// <summary>
        /// computes the total amount a student will pay after discounts.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="term"></param>
        /// <param name="resident_id"></param>
        /// <returns></returns>
        public virtual async Task<decimal?> FinalAmountDiscounted(int id, string term, string resident_id)
        {
            //getting the base amount before discount
            decimal baseAmount = (decimal)await GetTotalAmount(id, term);

    
            //get the discount from the dbcontext based on the resident type of a student.
            var discount = (from s in _context.StudentType
                            where s.ResidentId == resident_id
                            select s.Discount).FirstOrDefault();
            //final amount after discount
            decimal finalAmount = baseAmount - discount;

            return finalAmount;
                                    
            
        }


        /// <summary>
        /// Remove a course from list of registered courses
        /// </summary>
        /// <param name="id"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public virtual async Task<bool> Delete (int id, int studentId)
        {
            var enrollment = await (from e in _context.Enrollment
                                    where e.EnrollmentId == id && e.StudentId == studentId
                                    select e).FirstOrDefaultAsync ();

            if (enrollment == default)
            {
                return false;
            }

            _context.Enrollment.Remove (enrollment);
            await _context.SaveChangesAsync ();

            return true;
        }


        /// <summary>
        /// Register for a course
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="sectionId"></param>
        /// <returns></returns>
        public virtual async Task Add (int studentId, int sectionId)
        {
            _context.Enrollment.Add (new Enrollment
            {
                StudentId = studentId,
                SectId = sectionId
            });

            await _context.SaveChangesAsync ();
        }
    }
}
