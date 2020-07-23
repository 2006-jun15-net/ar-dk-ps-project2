using System.Collections.Generic;

namespace ClassRegistration.DataAccess.Entity
{
    public partial class StudentType : DataModel
    {
        public StudentType ()
        {
            Student = new HashSet<Student> ();
        }

        public string ResidentId { get; set; }
        public decimal Discount { get; set; }

        public virtual ICollection<Student> Student { get; set; }
    }
}
