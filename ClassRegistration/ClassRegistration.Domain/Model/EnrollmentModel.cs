namespace ClassRegistration.Domain.Model
{
    /// <summary>
    /// Business Logic Enrollment
    /// </summary>
    public class EnrollmentModel : BaseBusinessModel
    {

        public int EnrollmentId { get; set; }
        public int StudentId { get; set; }
        public int SectId { get; set; }
        public SectionModel Section { get; set; }

        public static int MinimumCredits (string term)
        {
            switch (term.ToLower ())
            {
                case "fall":
                case "spring":
                    return 12;

                case "summer":
                    return 8;
            }

            return -1;
        }
    }
}
