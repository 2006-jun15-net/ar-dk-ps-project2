using ClassRegistration.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClassRegistration.DataAccess.Entities;

namespace ClassRegistration.DataAccess.Interfaces
{
    public interface ISectionRepository
    {

        /// <summary>
        /// Search for all sections
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<SectionModel>> FindAll();


        /// <summary>
        /// To get access to a course and its reviews by instructor ID
        /// </summary>
        /// <param name="instructorId"></param>
        /// <returns></returns>
        Task<IEnumerable<Section>> FindByInstrId(int instructorId);
     }   
}
