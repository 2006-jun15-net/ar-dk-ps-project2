using System.Collections.Generic;

namespace ClassRegistration.Domain.Model
{
    public class InstructorModel : BaseBusinessModel
    {
        public InstructorModel ()
        {
            Section = new HashSet<SectionModel> ();
        }

        public int InstructorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int DeptId { get; set; }

        public ICollection<SectionModel> Section { get; set; }
    }
}





