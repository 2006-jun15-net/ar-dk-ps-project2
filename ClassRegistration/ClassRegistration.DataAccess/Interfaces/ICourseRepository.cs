using System;
using ClassRegistration.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClassRegistration.DataAccess.Interfaces
{
    public interface ICourseRepository
    {
        Task<IEnumerable<CourseModel>> FindByStudent (int studentId);
        Task<IEnumerable<Domain.Model.Course>> GetTheCourses ();
        Task<Domain.Model.Course> GetCourseByID (int id);
        Task<Domain.Model.Course> GetCourseByName (string name);
        Task<IEnumerable<Domain.Model.Course>> GetCourseByDepID (int id);
        Task<IEnumerable<Domain.Model.Course>> GetCourseByDepName (string name);
    }
}
