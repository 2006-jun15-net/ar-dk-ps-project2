using System.Threading.Tasks;

namespace ClassRegistration.Domain {

    public interface IEnrollmentRepository {

        Task<bool> Delete (int studentId, int enrollmentId);
    }
}
