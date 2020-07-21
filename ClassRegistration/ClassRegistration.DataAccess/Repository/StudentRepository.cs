using ClassRegistration.DataAccess.Entity;
using ClassRegistration.Domain;
using ClassRegistration.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ClassRegistration.DataAccess.Repository 
{
    public class StudentRepository : Repository, IStudentRepository 
    {
        public StudentRepository (Course_registration_dbContext context) : base (context) { }

        public StudentRepository () : this (null) { }

        public virtual async Task<StudentModel> FindById (int id) 
        {
            var students = from s in _context.Student
                           where s.StudentId == id
                           select s;

            return await students.Select (s => new StudentModel {
                Id = s.StudentId,
                Name = s.FirstName + " " + s.LastName
            }).FirstOrDefaultAsync ();
        }
    }
}
