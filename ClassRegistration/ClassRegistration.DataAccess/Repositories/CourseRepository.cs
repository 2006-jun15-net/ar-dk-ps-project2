using System;
using System.Collections.Generic;
using System.Linq;
using ClassRegistration.DataAccess.Entity;
using ClassRegistration.DataAccess.Interfaces;
using ClassRegistration.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;


namespace ClassRegistration.DataAccess.Repositories
{
    public class CourseRepository : GenericRepository<DataAccess.Entity.Course, Domain.Model.Course>, ICourseRepository
    {

        

        public CourseRepository(Course_registration_dbContext _context) : base (_context) 
        {
          
        }




        //get all the courses available
        public async Task<IEnumerable<Domain.Model.Course>> GetTheCourses()
        {
            //    var class = _dbContext.Course.Where(c => c.CourseId == courseId);
            //List<Course> classes = _context.Course.ToList();

            //var businessClasses = mapper.Map<IEnumerable<Domain.Model.Course>>(classes);
            //return businessClasses;

            List<Course> classes = await _context.Course.ToListAsync();

            return mapper.Map<IEnumerable<Domain.Model.Course>>(classes);
            
        }
    




        //get a course by ID
        public async Task<Domain.Model.Course> GetCourseByID(int id)
        {
            

            var searchedCourse = await _context.Course.FirstOrDefaultAsync(c => c.CourseId == id);

            return mapper.Map<Domain.Model.Course>(searchedCourse);
            
        }




        //get a course by its name
        public async Task<Domain.Model.Course> GetCourseByName(string name)
        {
            var searchedCourse = await _context.Course.FirstOrDefaultAsync(c => c.CourseName == name);

            return mapper.Map<Domain.Model.Course>(searchedCourse);
            
        }




        //get list of courses by Department ID or name
        public async Task<IEnumerable<Domain.Model.Course>> GetCourseByDepID(int id)
        {
            List<Course> searchedCourses = await _context.Course.Where(c => c.DeptId == id).ToListAsync();

            return mapper.Map<IEnumerable<Domain.Model.Course>>(searchedCourses);
            
        }

        public async Task<IEnumerable<Domain.Model.Course>> GetCourseByDepName(string name)
        {
            List<Course> searchedCourses = await _context.Course.Where(c => c.Dept.DeptName == name).ToListAsync();

            return mapper.Map<IEnumerable<Domain.Model.Course>>(searchedCourses);
           
        }






    }
}
