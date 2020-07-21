using ClassRegistration.DataAccess.Entity;
using ClassRegistration.Domain;
using ClassRegistration.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ClassRegistration.DataAccess.Repository
{
    public class StudentRepository : Repository<Student, StudentModel>, IStudentRepository
    {
        public StudentRepository (Course_registration_dbContext context) : base (context) { }

        public StudentRepository () : this (null) { }

        public virtual async Task<StudentModel> FindById (int id)
        {
            var student = await _context.Student.FirstOrDefaultAsync (s => s.StudentId == id);
            return _mapper.Map<StudentModel> (student);
        }
    }
}
