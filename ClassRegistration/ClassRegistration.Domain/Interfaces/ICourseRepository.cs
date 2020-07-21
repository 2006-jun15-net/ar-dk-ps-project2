using System;
using ClassRegistration.Domain.Model;
using System.Collections.Generic;

namespace ClassRegistration.Domain.Interfaces
{
    public interface ICourseRepository
    {
        IEnumerable<Domain.Model.Course> GetTheCourses();

        Domain.Model.Course GetCourseByID(int id);

        Domain.Model.Course GetCourseByName(string name);

        IEnumerable<Domain.Model.Course> GetCourseByDepID(int id);

        IEnumerable<Domain.Model.Course> GetCourseByDepName(string name);



    }
}
