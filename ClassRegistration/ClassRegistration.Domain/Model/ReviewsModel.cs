using System;

namespace ClassRegistration.Domain.Model
{
    /// <summary>
    /// Business Logic Reviews
    /// </summary>
    public class ReviewsModel : BaseBusinessModel
    {
        /// <summary>
        /// A review has an ID number
        /// </summary>
        public int ReviewId { get; set; }
        //public int Score { get; set; }


        /// <summary>
        /// A student can leave a review with a score
        /// </summary>
        private int _score;
        public int Score
        {
            get => _score;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("A review's score cannot be negative.", nameof(value));
                }
                _score = value;
            }
        }

        /// <summary>
        /// A student can leave some comments along with the review
        /// </summary>
        private string _text;
        public string Text 
        {
            get => _text;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("A review's comments cannot be empty or null.", nameof(value));
                }
                _text = value;
            }
        }

        /// <summary>
        /// When the review was submitted
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// The student who submitted the review
        /// </summary>
        public int StudentId { get; set; }

        /// <summary>
        /// The course the review was submitted for
        /// </summary>
        public int CourseId { get; set; }

        public CourseModel Course { get; set; }
        public StudentModel Student { get; set; }
    }
}





