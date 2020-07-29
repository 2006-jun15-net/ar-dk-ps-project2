using System;
using System.Collections.Generic;

namespace ClassRegistration.Domain.Model
{
    /// <summary>
    /// Business Logic Section
    /// </summary>
    public class SectionModel : BaseBusinessModel
    {
        public SectionModel ()
        {
            Enrollment = new HashSet<EnrollmentModel> ();
        }

        public int SectId { get; set; }
        public int CourseId { get; set; }
        public int InstructorId { get; set; }
        public string Term { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public CourseModel Course { get; set; }
        public InstructorModel Instructor { get; set; }
        public ICollection<EnrollmentModel> Enrollment { get; set; }
    }
}
