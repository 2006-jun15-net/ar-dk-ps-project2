//using System;
//using System.Collections.Generic;

//namespace ClassRegistration.DataAccess.Entity
//{
//    public partial class Department : DataModel
//    {
//        public Department()
//        {
//            Course = new HashSet<Course>();
//            Instructor = new HashSet<Instructor>();
//        }

//        public int DeptId { get; set; }
//        public string DeptName { get; set; }
//        public int CollegeId { get; set; }

//        public virtual College College { get; set; }
//        public virtual ICollection<Course> Course { get; set; }
//        public virtual ICollection<Instructor> Instructor { get; set; }
//    }
//}
