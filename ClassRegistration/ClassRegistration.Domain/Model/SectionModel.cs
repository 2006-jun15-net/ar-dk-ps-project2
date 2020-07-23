using System;
using System.Collections.Generic;

namespace ClassRegistration.Domain.Model
{
    public class SectionModel : BaseBusinessModel
    {
        public SectionModel()
        {
            Enrollment = new HashSet<Enrollment>();
        }

        public int SectId { get; set; }
        public int CourseId { get; set; }
        public int InstructorId { get; set; }
        public string Term { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public CourseModel Course { get; set; } 
        public virtual InstructorModel Instructor { get; set; }
        public virtual SemesterModel Semester { get; set; }
        public virtual ICollection<Enrollment> Enrollment { get; set; }
    }
}
 