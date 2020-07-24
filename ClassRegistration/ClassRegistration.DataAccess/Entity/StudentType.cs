using System.Collections.Generic;

namespace ClassRegistration.DataAccess.Entity
{
    /// <summary>
    /// Data Access StudentType
    /// </summary>
    public partial class StudentType : DataModel
    {
        public StudentType ()
        {
            Student = new HashSet<Student> ();
        }

        /// <summary>
        /// A on-campus resident has an ID number
        /// </summary>
        public string ResidentId { get; set; }
        public decimal Discount { get; set; }

        public virtual ICollection<Student> Student { get; set; }
    }
}
