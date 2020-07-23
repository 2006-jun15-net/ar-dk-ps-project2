using System;
using System.Collections.Generic;

namespace ClassRegistration.DataAccess.Entity
{
    public partial class Student : DataModel
    {
        public Student ()
        {
            Enrollment = new HashSet<Enrollment> ();
        }

        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int DeptId { get; set; }

        public Department Department { get; set; }
        public ICollection<Enrollment> Enrollment { get; set; }
    }
}
