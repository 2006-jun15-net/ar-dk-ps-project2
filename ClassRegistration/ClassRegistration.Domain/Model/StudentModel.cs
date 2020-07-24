using System;
using System.Collections.Generic;

namespace ClassRegistration.Domain.Model
{
    /// <summary>
    /// Business Logic Student
    /// </summary>
    public class StudentModel : BaseBusinessModel
    {
        public StudentModel ()
        {
            Enrollment = new HashSet<EnrollmentModel> ();
            Reviews = new HashSet<ReviewsModel> ();
        }

        /// <summary>
        /// A student's ID number
        /// </summary>
        public int StudentId { get; set; }

        //public string FirstName { get; set; }


        /// <summary>
        /// A student's first name
        /// </summary>
        private string _firstname;
        public string FirstName
        {
            get => _firstname;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Student's first name name cannot be empty or null.", nameof(value));
                }
                _firstname = value;
            }

        }



        //public string LastName { get; set; }


        /// <summary>
        /// A student's last name
        /// </summary>
        private string _lastname;
        public string LastName
        {
            get => _lastname;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Student's last name name cannot be empty or null.", nameof(value));
                }
                _lastname = value;
            }

        }


        /// <summary>
        /// Department the student has courses in
        /// </summary>
        public int DeptId { get; set; }

        public ICollection<EnrollmentModel> Enrollment { get; set; }
        public ICollection<ReviewsModel> Reviews { get; set; }

        public string Name
        {
            get => FirstName + " " + LastName;
            set
            {
                var names = value.Split (" ");

                FirstName = names[0];
                LastName = names[1];
            }
        }

        public bool CreditRequirementsMet
        {
            get
            {
                int majorCredits = 0;
                int nonMajorCredits = 0;

                foreach (var enrollment in Enrollment)
                {
                    var course = enrollment.Section.Course;
                    var credits = Convert.ToInt32 (course.Credits);

                    if (course.DeptId == DeptId)
                    {
                        majorCredits += credits;
                    }

                    else
                    {
                        nonMajorCredits += credits;
                    }
                }

                return majorCredits >= 6 && nonMajorCredits >= 6;
            }
        }
    }
}
