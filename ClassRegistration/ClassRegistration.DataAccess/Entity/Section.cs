namespace ClassRegistration.DataAccess.Entity
{
    public partial class Section : DataModel
    {
        public Section ()
        {
            Enrollment = new HashSet<Enrollment> ();
        }

        public int SectId { get; set; }
        public int CourseId { get; set; }
        public int InstructorId { get; set; }
        public string Term { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public Course Course { get; set; }
        public Instructor Instructor { get; set; }
        public Semester TermNavigation { get; set; }
        public ICollection<Enrollment> Enrollment { get; set; }
    }
}
