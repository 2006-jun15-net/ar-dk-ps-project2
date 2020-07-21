using ClassRegistration.Domain.Model;
using System.Threading.Tasks;

namespace ClassRegistration.Domain
{
    public interface IStudentRepository 
    {
        public Task<StudentModel> FindById (int id);
    }
}
