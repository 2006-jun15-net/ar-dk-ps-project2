using ClassRegistration.DataAccess.Entity;
using ClassRegistration.DataAccess.Interfaces;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ClassRegistration.DataAccess.Repository
{
    public class StudentTypeRepository : Repository, IStudentTypeRepository
    {
        public StudentTypeRepository (Course_registration_dbContext context) : base (context) { }

        public StudentTypeRepository () : this (null) { }

        public virtual async Task<decimal> FindDiscount (string residentId)
        {
            var discount = await (from s in _context.StudentType
                            where s.ResidentId.ToLower () == residentId.ToLower ()
                            select s.Discount).FirstOrDefaultAsync ();

            return discount;
        }
    }
}
