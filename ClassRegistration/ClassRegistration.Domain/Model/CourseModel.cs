using System;
using System.Collections.Generic;

namespace ClassRegistration.Domain.Model
{
    /// <summary>
    /// Business Logic Course
    /// </summary>
    public class CourseModel : BaseBusinessModel
    {
        public CourseModel ()
        {
            Reviews = new HashSet<ReviewsModel>();
        }

        
        private string _courseName;

        /// <summary>
        /// A course has a name
        /// </summary>
        public string CourseName
        {
            get => _courseName;
            set
            {
                if (string.IsNullOrEmpty (value))
                {
                    throw new ArgumentException ("Course name cannot be empty or null.", nameof (value));
                }
                _courseName = value;
            }
        }

        /// <summary>
        /// A course has an ID number
        /// </summary>
        public int CourseId { get; set; }

        
        public int StudentId { get; set; }

        /// <summary>
        /// The department the course is in
        /// </summary>
        public int DeptId { get; set; }

        /// <summary>
        /// Points the course is worth
        /// </summary>
        //public int? Credits { get; set; }

        private int? _credits;
        public int? Credits
        {
            get => _credits;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Course cannot be worth zero or negative credits.", nameof(value));
                }
                _credits = value;
            }
        }


        /// <summary>
        /// How much the course costs
        /// </summary>
        //public decimal Fees { get; set; }

        private decimal _fees;
        public decimal Fees
        {
            get => _fees;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Course fee cannot be zero.", nameof(value));
                }
                _fees = value;
            }
        }


        /// <summary>
        /// List of reviews for a course
        /// </summary>
        public ICollection<ReviewsModel> Reviews { get; set; }
    }
}
