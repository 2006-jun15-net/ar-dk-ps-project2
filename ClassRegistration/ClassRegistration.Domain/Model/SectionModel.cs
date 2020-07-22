using System;

namespace ClassRegistration.Domain.Model
{
    public class SectionModel : BaseBusinessModel
    {
        public int SectId { get; set; }
        public int CourseId { get; set; }
        public int InstructorId { get; set; }
        public string Term { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public CourseModel Course { get; set; }
    }
}
