using System;
using System.Collections.Generic;

namespace ClassRegistration.DataAccess.Entity
{
    public partial class Section
    {
        public Section()
        {
            Enrollment = new HashSet<Enrollment>();
        }

        public int SectId { get; set; }
        public int CourseId { get; set; }
        public int InstructorId { get; set; }
        public string Term { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public virtual Course Course { get; set; }
        public virtual Instructor Instructor { get; set; }
        public virtual Semester TermNavigation { get; set; }
        public virtual ICollection<Enrollment> Enrollment { get; set; }
    }
}
