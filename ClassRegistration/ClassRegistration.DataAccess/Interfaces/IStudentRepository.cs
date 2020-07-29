using System.Collections.Generic;
using ClassRegistration.DataAccess.Pagination;
using ClassRegistration.Domain.Model;
using System.Threading.Tasks;

namespace ClassRegistration.Domain
{
    public interface IStudentRepository
    {
        /// <summary>
        /// Search a student by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<StudentModel> FindById (int id);
        Task<IEnumerable<StudentModel>> FindByFirstname (string FirstName);
        Task<IEnumerable<StudentModel>> FindByLastname (string LastName);
        Task<StudentModel> FindByName (string FirstName, string LastName);
    }
}
