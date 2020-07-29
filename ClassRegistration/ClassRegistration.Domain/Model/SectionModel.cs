using System;

namespace ClassRegistration.Domain.Model
{
    /// <summary>
    /// Business Logic Section
    /// </summary>
    public class SectionModel : BaseBusinessModel
    {
        public int SectId { get; set; }
        public int InstructorId { get; set; }
        public CourseModel Course { get; set; }
        public string Term { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
