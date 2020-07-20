using ClassRegistration.DataAccess.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using ClassRegistration.Domain;
using ClassRegistration.Domain.Model;
using System.Collections.Generic;
using System;

namespace ClassRegistration.DataAccess.Repository {

    public class CourseRepository : Repository, ICourseRepository {

        public CourseRepository (Course_registration_dbContext context) : base (context) { }

        public CourseRepository () : this (null) {}

        public virtual async Task<IEnumerable<CourseModel>> FindByStudent (int studentId) {

            var courses = from c in _context.Course
                          join s in _context.Section on c.CourseId equals s.CourseId 
                          join e in _context.Enrollment on s.SectId equals e.SectId
                          where e.StudentId == studentId
                          select c;

            return await courses.Select (
                                c => new CourseModel {
                                    CourseName = c.CourseName,
                                    StudentId = studentId
                                }).ToListAsync ();
        }
    }
}
