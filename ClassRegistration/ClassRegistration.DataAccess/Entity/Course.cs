using System.Collections.Generic;

namespace ClassRegistration.DataAccess.Entity
{
    /// <summary>
    /// Data Access Course
    /// </summary>
    public partial class Course : DataModel
    {
        public Course ()
        {
            Reviews = new HashSet<Reviews> ();
            Section = new HashSet<Section> ();
        }

        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int DeptId { get; set; }
        public int? Credits { get; set; }
        public decimal Fees { get; set; }

        public virtual Department Dept { get; set; }
        public virtual ICollection<Reviews> Reviews { get; set; }
        public virtual ICollection<Section> Section { get; set; }
    }
}
