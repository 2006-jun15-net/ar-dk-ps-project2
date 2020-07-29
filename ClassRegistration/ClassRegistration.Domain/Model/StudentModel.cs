using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ClassRegistration.Domain.Model
{
    /// <summary>
    /// Business Logic Student
    /// </summary>
    public class StudentModel : BaseBusinessModel
    {
        private readonly string[] AllowedResIds = new string[] { "in-state", "out-of-state" };

        private string _lastname;
        private string _firstname;
        private string _residentId;

        public StudentModel ()
        {
            Enrollment = new HashSet<EnrollmentModel> ();
        }

        public ICollection<EnrollmentModel> Enrollment { get; set; }

        /// <summary>
        /// A student's ID number
        /// </summary>
        public int StudentId { get; set; }

        /// <summary>
        /// A student's residency type
        /// </summary>
        public string ResidentId
        {
            get => _residentId;
            set
            {
                if (!AllowedResIds.Contains (value.ToLower ()))
                {
                    throw new ArgumentException ("Invalid resident Id.", nameof (value));
                }

                _residentId = value;
            }
        }

        /// <summary>
        /// A student's first name
        /// </summary>
        [RegularExpression (@"^[A-Z][a-z]+")]
        public string FirstName
        {
            get => _firstname;
            set
            {
                if (string.IsNullOrEmpty (value))
                {
                    throw new ArgumentException ("Student's first name cannot be empty or null.", nameof (value));
                }
                _firstname = value;
            }
        }

        /// <summary>
        /// A student's last name
        /// </summary>
        [RegularExpression (@"^[A-Z][a-z]+")]
        public string LastName
        {
            get => _lastname;
            set
            {
                if (string.IsNullOrEmpty (value))
                {
                    throw new ArgumentException ("Student's last name cannot be empty or null.", nameof (value));
                }
                _lastname = value;
            }
        }

        /// <summary>
        /// Department the student has courses in
        /// </summary>
        public int DeptId { get; set; }

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
