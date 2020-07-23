namespace ClassRegistration.DataAccess.Entity
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
