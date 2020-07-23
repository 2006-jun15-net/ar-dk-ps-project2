using System;

namespace ClassRegistration.DataAccess.Entity
{
    public partial class Reviews : DataModel
    {
        public int ReviewId { get; set; }
        public int Score { get; set; }
        public string Text { get; set; }
        public int CourseId { get; set; }
        public int StudentId { get; set; }
        public DateTime Date { get; set; }

        public virtual Course Course { get; set; }
        public virtual Student Student { get; set; }
    }
}
