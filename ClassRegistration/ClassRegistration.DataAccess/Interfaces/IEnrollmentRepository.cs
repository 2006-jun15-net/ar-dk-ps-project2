using System.Threading.Tasks;

namespace ClassRegistration.DataAccess.Interfaces
{
    /// <summary>
    /// This interface includes common functionality that is needed by the Enrollment repository
    /// </summary>
    public interface IEnrollmentRepository
    {
        /// <summary>
        /// This method returns the total credits of a student with a specified id and semester
        /// </summary>
        /// <param name="id"></param>
        /// <param name="term"></param>
        /// <returns></returns>
        Task<int?> GetCredits (int id, string term);

        /// <summary>
        /// This methods gets the total amount of registered courses of a student in a specified term.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="term"></param>
        /// <returns></returns>
        Task<decimal?> GetTotalAmount (int id, string term);

        /// <summary>
        /// Remove a course from list of registered courses
        /// </summary>
        /// <param name="id"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        Task<bool> Delete (int id, int studentId);

        /// <summary>
        /// Register for a course
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="sectionId"></param>
        /// <returns></returns>
        Task Add (int studentId, int sectionId);
    }
}
