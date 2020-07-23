using System;
using System.Collections.Generic;

namespace ClassRegistration.DataAccess.Entities
{
    public partial class Student : DataModel
    {
        public Student()
        {
            Enrollment = new HashSet<Enrollment>();
            Reviews = new HashSet<Reviews>();
        }

        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int DeptId { get; set; }

        public virtual Department Dept { get; set; }
        public virtual ICollection<Enrollment> Enrollment { get; set; }
        public virtual ICollection<Reviews> Reviews { get; set; }
    }
}
