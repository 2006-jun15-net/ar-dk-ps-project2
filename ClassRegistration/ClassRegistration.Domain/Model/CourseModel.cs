using System;
using System.Collections.Generic;

namespace ClassRegistration.Domain.Model
{
    public class CourseModel : BaseBusinessModel
    {
        public CourseModel ()
        {
            Reviews = new HashSet<ReviewsModel>();
        }

        private string _courseName;

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

        public int CourseId { get; set; }
        public int StudentId { get; set; }
        public int DeptId { get; set; }
        public int? Credits { get; set; }
        public decimal Fees { get; set; }

        public ICollection<ReviewsModel> Reviews { get; set; }
    }
}
