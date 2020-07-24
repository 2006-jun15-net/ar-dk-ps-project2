using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ClassRegistration.DataAccess.Interfaces
{
    public interface IStudentTypeRepository
    {
        Task<decimal> FindDiscount (string residentId);
    }
}
