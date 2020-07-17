using System;
using System.Collections.Generic;
using System.Linq;
using ClassRegistration.DataAccess.Entity;
using ClassRegistration.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ClassRegistration.Domain.Repositories
{
    public class CourseRepository
    {

        private readonly Course_registration_dbContext _dbContext;

        public CourseRepository(Course_registration_dbContext thecontext)
        {
            _dbContext = thecontext;
        }


        //get all the courses available
        public IEnumerable<DataAccess.Entity.Course> GetTheCourses()
        {
            //    var class = _dbContext.Course.Where(c => c.CourseId == courseId);
            List<DataAccess.Entity.Course> classes = _dbContext.Course.ToList();
            return classes;
        }

       

        //get a course by ID
        public DataAccess.Entity.Course GetCourseByID(int id)
        {
            var searchedCourse = _dbContext.Course.FirstOrDefault(c => c.CourseId == id);
            return searchedCourse;
        }


    }
}
