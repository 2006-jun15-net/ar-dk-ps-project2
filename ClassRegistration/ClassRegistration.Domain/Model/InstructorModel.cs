using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClassRegistration.Domain.Model
{
    /// <summary>
    /// Business Logic Instructor
    /// </summary>
    public class InstructorModel : BaseBusinessModel
    {
        private string _firstname;
        private string _lastname;

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
        [RegularExpression (@"^[A-Z][a-z]+")]
        public string FirstName
        {
            get => _firstname;
            set
            {
                if (string.IsNullOrEmpty (value))
                {
                    throw new ArgumentException ("Instructor's first name name cannot be empty or null.", nameof (value));
                }
                _firstname = value;
            }

        }

        /// <summary>
        /// A prof has a last name
        /// </summary>
        [RegularExpression (@"^[A-Z][a-z]+")]
        public string LastName
        {
            get => _lastname;
            set
            {
                if (string.IsNullOrEmpty (value))
                {
                    throw new ArgumentException ("Instructor's last name name cannot be empty or null.", nameof (value));
                }
                _lastname = value;
            }

        }

        /// <summary>
        /// The department the prof teaches in
        /// </summary>
        public int DeptId { get; set; }

        public ICollection<SectionModel> Section { get; set; }
    }
}





