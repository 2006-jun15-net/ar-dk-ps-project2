using System;
using System.Collections.Generic;

namespace ClassRegistration.DataAccess.Entities
{
    public partial class Enrollment : DataModel
    {
        public int EnrollmentId { get; set; }
        public int StudentId { get; set; }
        public int SectId { get; set; }

        public virtual Section Sect { get; set; }
        public virtual Student Student { get; set; }
    }
}
