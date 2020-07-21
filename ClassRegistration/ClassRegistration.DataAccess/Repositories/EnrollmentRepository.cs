
using ClassRegistration.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassRegistration.DataAccess.Repositories;
using ClassRegistration.DataAccess.Interfaces;

namespace ClassRegistration.DataAccess.Repositories
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        /// <summary>
        /// A private field for the database entity
        /// </summary>
        private Course_registration_dbContext _dbContext;

        /// <summary>
        /// A constructor that intializes the database entity.
        /// </summary>
        /// <param name="dbcontext"></param>
        public EnrollmentRepository(Course_registration_dbContext dbcontext)
        {
            _dbContext = dbcontext;


        }

       
        /// <summary>
        /// This method returns the total credits of a student with a specified id and semester
        /// </summary>
        /// <param name="id"></param>
        /// <param name="term"></param>
        /// <returns></returns>
        public int GetCredits(int id, string term)
        {
            int totalCredits = (int)(from c in _dbContext.Course
                                join s in _dbContext.Section on c.CourseId
                                equals s.CourseId
                                join e in _dbContext.Enrollment on s.SectId equals e.SectId
                                where e.StudentId == id && s.Term == term   //enrollments of a particular student with their respective semester.

                                select c.Credits).Sum();
            return totalCredits;

        }

        /// <summary>
        /// This method saves chnages to the database context.
        /// </summary>
        public void Save()
        {
            _dbContext.SaveChanges();
        }

       
    }
}
