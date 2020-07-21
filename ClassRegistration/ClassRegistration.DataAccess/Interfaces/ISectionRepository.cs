using ClassRegistration.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClassRegistration.DataAccess.Interfaces
{
    public interface ISectionRepository
    {
        Task<IEnumerable<SectionModel>> FindAll ();
        Task<IEnumerable<SectionModel>> FindById (int id);
    }
}
