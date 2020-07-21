using System;
using System.Collections.Generic;

namespace ClassRegistration.DataAccess.Entity
{
    public partial class Semester : DataModel
    {
        public Semester()
        {
            Section = new HashSet<Section>();
        }

        public string Term { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }

        public virtual ICollection<Section> Section { get; set; }
    }
}
