using System;
using ClassRegistration.Domain.Model;
using System.Collections.Generic;



namespace ClassRegistration.Domain.Interfaces
{
    public interface ISectionRepository
    {

        public IEnumerable<Domain.Model.Section> GetTheSections();

        public IEnumerable<Domain.Model.Section> GetSectionByInstID(int id);
    }
}
