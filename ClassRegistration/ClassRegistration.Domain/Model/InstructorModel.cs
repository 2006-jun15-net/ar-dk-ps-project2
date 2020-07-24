using System;
using System.Collections.Generic;

namespace ClassRegistration.Domain.Model
{
    /// <summary>
    /// Business Logic Instructor
    /// </summary>
    public class InstructorModel : BaseBusinessModel
    {
        public InstructorModel ()
        {
            Section = new HashSet<SectionModel> ();
        }


        /// <summary>
        /// An instructor has an ID number
        /// </summary>
        public int InstructorId { get; set; }


        /// <summary>
        /// A prof has a first name
        /// </summary>
        private string _firstname;
        public string FirstName
        {
            get => _firstname;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Instructor's first name name cannot be empty or null.", nameof(value));
                }
                _firstname = value;
            }

        }


        //public string FirstName { get; set; }

        /// <summary>
        /// A prof has a last name
        /// </summary>
        private string _lastname; 
        public string LastName
        {
            get => _lastname;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Instructor's last name name cannot be empty or null.", nameof(value));
                }
                _lastname = value;
            }

        }

        //public string LastName { get; set; }

        /// <summary>
        /// The department the prof teaches in
        /// </summary>
        public int DeptId { get; set; }

        public ICollection<SectionModel> Section { get; set; }
    }
}





