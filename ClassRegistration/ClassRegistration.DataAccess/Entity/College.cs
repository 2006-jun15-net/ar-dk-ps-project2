using System.Collections.Generic;

namespace ClassRegistration.DataAccess.Entity
{
    /// <summary>
    /// Data Access College
    /// </summary>
    public partial class College : DataModel
    {
        public College ()
        {
            Department = new HashSet<Department> ();
        }

        public int CollegeId { get; set; }
        public string CollegeName { get; set; }
        public string Address { get; set; }

        public virtual ICollection<Department> Department { get; set; }
    }
}
