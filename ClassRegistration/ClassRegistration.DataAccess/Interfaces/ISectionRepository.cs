using ClassRegistration.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClassRegistration.DataAccess.Interfaces
{
    public interface ISectionRepository
    {
        Task<SectionModel> FindById (int id);
        Task<IEnumerable<SectionModel>> FindAll ();
        Task<IEnumerable<SectionModel>> FindByInstrId (int instructorId);
    }
}
