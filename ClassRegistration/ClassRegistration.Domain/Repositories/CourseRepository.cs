using System;
using System.Collections.Generic;
using System.Linq;
using ClassRegistration.DataAccess.Entity;
using ClassRegistration.Domain.Interfaces;
using ClassRegistration.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;


namespace ClassRegistration.Domain.Repositories
{
    public class CourseRepository : GenericRepository<DataAccess.Entity.Course, Domain.Model.Course>, ICourseRepository
    {

        

        public CourseRepository(Course_registration_dbContext _context) : base (_context) 
        {
          
        }




        //get all the courses available
        public IEnumerable<Model.Course> GetTheCourses()
        {
            //    var class = _dbContext.Course.Where(c => c.CourseId == courseId);
            List<Course> classes = _context.Course.ToList();

            var businessClasses = mapper.Map<IEnumerable<Domain.Model.Course>>(classes);
            return businessClasses;
        }




        //get a course by ID
        public Domain.Model.Course GetCourseByID(int id)
        {
            var searchedCourse = _context.Course.FirstOrDefault(c => c.CourseId == id);

            var businessSearchedCourse = mapper.Map<Domain.Model.Course>(searchedCourse);
            return businessSearchedCourse;
        }




        //get a course by its name
        public Domain.Model.Course GetCourseByName(string name)
        {
            var searchedCourse = _context.Course.FirstOrDefault(c => c.CourseName == name);

            var businessSearchedCourse = mapper.Map<Domain.Model.Course>(searchedCourse);
            return businessSearchedCourse;
        }




        //get list of courses by Department ID or name
        public IEnumerable<Domain.Model.Course> GetCourseByDepID(int id)
        {
            List<Course> searchedCourses = _context.Course.Where(c => c.DeptId == id).ToList();

            var businessSearchedCourse = mapper.Map<IEnumerable<Domain.Model.Course>>(searchedCourses);
            return businessSearchedCourse;
        }

        public IEnumerable<Domain.Model.Course> GetCourseByDepName(string name)
        {
            List<Course> searchedCourses = _context.Course.Where(c => c.Dept.DeptName == name).ToList();

            var businessSearchedCourse = mapper.Map<IEnumerable<Domain.Model.Course>>(searchedCourses);
            return businessSearchedCourse;
        }






    }
}
