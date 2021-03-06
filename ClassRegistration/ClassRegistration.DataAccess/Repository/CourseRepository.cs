﻿using ClassRegistration.DataAccess.Entity;
using ClassRegistration.DataAccess.Interfaces;
using ClassRegistration.DataAccess.Pagination;
using ClassRegistration.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassRegistration.DataAccess.Repository
{
    public class CourseRepository : Repository, ICourseRepository
    {
        public CourseRepository (Course_registration_dbContext context) : base (context) { }

        public CourseRepository () : this (null) { }

        /// <summary>
        /// Get all courses available
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IEnumerable<CourseModel>> FindAll (ModelPagination coursePagination)
        {

            var classes = await _context.Course
                .Skip ((coursePagination.PageNumber - 1) * coursePagination.PageSize) //skipping some pages 
                .Take (coursePagination.PageSize)
                .ToListAsync ();


            return _mapper.Map<IEnumerable<CourseModel>> (classes);
        }

        /// <summary>
        /// Search a course by its ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<CourseModel> FindById (int id)
        {
            var searchedCourse = await _context.Course.FirstOrDefaultAsync (c => c.CourseId == id);
            return _mapper.Map<CourseModel> (searchedCourse);
        }

        /// <summary>
        /// Search a course by its name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public virtual async Task<CourseModel> FindByName (string name)
        {
            var searchedCourse = await _context.Course.Include (c => c.Reviews).FirstOrDefaultAsync (c => c.CourseName == name);
            return _mapper.Map<CourseModel> (searchedCourse);
        }

        /// <summary>
        /// Search for courses by department ID
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<CourseModel>> FindByDeptId (int deptId)
        {
            var searchedCourses = await _context.Course.Where (c => c.DeptId == deptId).ToListAsync ();
            return _mapper.Map<IEnumerable<CourseModel>> (searchedCourses);
        }

        /// <summary>
        /// search for courses by department name
        /// </summary>
        /// <param name="deptName"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<CourseModel>> FindByDeptName (string deptName)
        {
            List<Course> searchedCourses = await _context.Course.Where (c => c.Dept.DeptName == deptName).ToListAsync ();
            return _mapper.Map<IEnumerable<CourseModel>> (searchedCourses);
        }

        public virtual async Task<IEnumerable<CourseModel>> FindByStudent (int studentId)
        {
            var courses = await (from c in _context.Course
                                 join s in _context.Section on c.CourseId equals s.CourseId
                                 join e in _context.Enrollment on s.SectId equals e.SectId
                                 where e.StudentId == studentId
                                 select c).ToListAsync ();

            return _mapper.Map<IEnumerable<CourseModel>> (courses);
        }
    }
}
