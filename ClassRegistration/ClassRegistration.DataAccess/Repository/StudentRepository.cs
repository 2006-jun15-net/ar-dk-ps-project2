using ClassRegistration.DataAccess.Entity;
using ClassRegistration.Domain;
using ClassRegistration.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ClassRegistration.DataAccess.Repository
{
    public class StudentRepository : Repository, IStudentRepository
    {
        public StudentRepository (Course_registration_dbContext context) : base (context) { }

        public StudentRepository () : this (null) { }

        /// <summary>
        /// Search for a student by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<StudentModel> FindById (int id)
        {
            var student = await _context.Student.FirstOrDefaultAsync (s => s.StudentId == id);
            return _mapper.Map<StudentModel> (student);
        }


        public virtual async Task<StudentModel> FindByName(string lastname)
        {
            var student = await _context.Student.FirstOrDefaultAsync(s => s.LastName == lastname);
            return _mapper.Map<StudentModel>(student);
        }
    }
}
