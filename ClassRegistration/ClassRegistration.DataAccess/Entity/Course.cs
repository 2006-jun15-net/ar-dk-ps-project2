using System.Collections.Generic;

namespace ClassRegistration.DataAccess.Entity
{
    public partial class Course : DataModel
    {
        public Course ()
        {
            Section = new HashSet<Section> ();
        }

        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int DeptId { get; set; }
        public int? Credits { get; set; }
        public decimal Fees { get; set; }

        public virtual Department Dept { get; set; }
        public virtual ICollection<Section> Section { get; set; }
    }
}
