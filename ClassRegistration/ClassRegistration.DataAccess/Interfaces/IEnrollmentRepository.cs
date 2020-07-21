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
        Task<bool> Delete (int studentId, int enrollmentId);
        Task<bool> Add (EnrollmentModel enrollmentModel);
        Task Save ();
    }
}
