using ClassRegistration.Domain.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClassRegistration.DataAccess.Interfaces
{
    public interface ISectionRepository
    {
        Task<SectionModel> FindById (int id);

        /// <summary>
        /// Search for all sections
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<SectionModel>> FindAll ();

        /// <summary>
        /// To get access to a course and its reviews by instructor ID
        /// </summary>
        /// <param name="instructorId"></param>
        /// <returns></returns>
        Task<IEnumerable<SectionModel>> FindByInstrId (int instructorId);

        Task<bool> Add (int instructorId, int courseId, string term, TimeSpan start, TimeSpan end);
    }
}
