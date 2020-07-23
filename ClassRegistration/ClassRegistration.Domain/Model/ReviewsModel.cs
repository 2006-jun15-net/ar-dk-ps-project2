using System;
using System.Collections.Generic;



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

        public virtual CourseModel Course { get; set; }
        public virtual StudentModel Student { get; set; }
    }
}





  