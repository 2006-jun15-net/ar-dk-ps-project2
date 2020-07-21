using ClassRegistration.Domain.Model;
using System.Threading.Tasks;

namespace ClassRegistration.Domain
{
    public interface IStudentRepository
    {
        Task<StudentModel> FindById (int id);
    }
}
