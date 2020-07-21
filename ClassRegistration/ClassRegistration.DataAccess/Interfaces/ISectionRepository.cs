using System;
using ClassRegistration.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClassRegistration.DataAccess.Interfaces
{
    public interface ISectionRepository
    {

        IEnumerable<Domain.Model.Section> GetTheSections();

        Task<IEnumerable<Domain.Model.Section>> GetSectionByInstID(int id);
    }
}
