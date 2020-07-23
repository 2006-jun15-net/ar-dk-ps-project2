using System;

namespace ClassRegistration.Domain.Model
{
    public class ReviewsModel : BaseBusinessModel
    {
        public int ReviewId { get; set; }
        public int Score { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }

        public CourseModel Course { get; set; }
        public StudentModel Student { get; set; }
    }
}





