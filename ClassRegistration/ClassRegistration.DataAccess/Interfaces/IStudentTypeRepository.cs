using System.Threading.Tasks;

namespace ClassRegistration.DataAccess.Interfaces
{
    public interface IStudentTypeRepository
    {
        Task<decimal> FindDiscount (string residentId);
    }
}
