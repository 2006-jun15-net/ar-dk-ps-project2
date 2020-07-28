using ClassRegistration.DataAccess.Entity;
using ClassRegistration.Domain;
using ClassRegistration.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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

        public virtual async Task<IEnumerable<StudentModel>> FindByFirstname (string FirstName)
        {
            var students = await _context.Student.Where (s => s.FirstName == FirstName).ToListAsync ();
            return _mapper.Map<IEnumerable<StudentModel>> (students);
        }

        public virtual async Task<IEnumerable<StudentModel>> FindByLastname (string LastName)
        {
            var students = await _context.Student.Where (s => s.LastName == LastName).ToListAsync ();
            return _mapper.Map<IEnumerable<StudentModel>> (students);
        }

        public virtual async Task<StudentModel> FindByName (string FirstName, string LastName)
        {
            var student = await _context.Student.FirstOrDefaultAsync (
                s => s.FirstName == FirstName && s.LastName == LastName
            );
            return _mapper.Map<StudentModel> (student);
        }
    }
}
