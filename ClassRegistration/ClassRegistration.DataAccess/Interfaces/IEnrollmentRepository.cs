using ClassRegistration.Domain.Model;
using System.Threading.Tasks;

namespace ClassRegistration.DataAccess.Interfaces
{
    /// <summary>
    /// This interface includes common functionality that is needed by the Enrollment repository
    /// </summary>
    public interface IEnrollmentRepository
    {
        Task<int?> GetCredits (int id, string term);

        Task<decimal?> GetTotalAmount(int id, string term);
        Task<bool> Delete (int id, int studentId);
        Task Add (int studentId, int sectionId);
    }
}
