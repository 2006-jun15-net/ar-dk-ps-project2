using System;
using System.Text;
using System.Collections.Generic;
using ClassRegistration.DataAccess.Entity;

namespace ClassRegistration.Domain
{
    public class Course
    {


        public Course()
        {
            Section = new HashSet<Section>();
        }

        private string _courseName;


        public string CourseName
        {
            get => _courseName;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Course name cannot be empty or null.", nameof(value));
                }
                _courseName = value;
            }
        }

        public int CourseId { get; set; }
        //public string CourseName { get; set; }



        public int DeptId { get; set; }
        public int? Credits { get; set; }
        public decimal Fees { get; set; }

        public virtual Department Dept { get; set; }
        public virtual ICollection<Section> Section { get; set; }
    }
}

