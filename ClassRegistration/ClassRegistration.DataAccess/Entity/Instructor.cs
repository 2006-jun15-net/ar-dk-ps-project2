//using System;
//using System.Collections.Generic;

//namespace ClassRegistration.DataAccess.Entity
//{
//    public partial class Instructor : DataModel
//    {
//        public Instructor()
//        {
//            Section = new HashSet<Section>();
//        }

//        public int InstructorId { get; set; }
//        public string FirstName { get; set; }
//        public string LastName { get; set; }
//        public int DeptId { get; set; }

//        public virtual Department Dept { get; set; }
//        public virtual ICollection<Section> Section { get; set; }
//    }
//}
