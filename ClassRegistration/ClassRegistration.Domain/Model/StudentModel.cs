using System;
using System.Collections.Generic;

namespace ClassRegistration.Domain.Model
{
    public class StudentModel : BaseBusinessModel
    {
        public StudentModel ()
        {
            Enrollment = new HashSet<EnrollmentModel> ();
        }

        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int DeptId { get; set; }

        public ICollection<EnrollmentModel> Enrollment;

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
