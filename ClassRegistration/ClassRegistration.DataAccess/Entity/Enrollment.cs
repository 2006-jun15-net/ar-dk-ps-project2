using System;
using System.Collections.Generic;

namespace ClassRegistration.DataAccess.Entity
{
    public partial class Enrollment : DataModel
    {
        public int EnrollmentId { get; set; }
        public int StudentId { get; set; }
        public int SectId { get; set; }

        public Section Sect { get; set; }
        public Student Student { get; set; }
    }
}
