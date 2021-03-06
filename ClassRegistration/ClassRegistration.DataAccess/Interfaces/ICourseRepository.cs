using ClassRegistration.DataAccess.Pagination;
using ClassRegistration.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClassRegistration.DataAccess.Interfaces
{
    public interface ICourseRepository
    {
        /// <summary>
        /// gets some courses using pagination.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<CourseModel>> FindAll (ModelPagination coursePagination);

        /// <summary>
        /// Search a course by course ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<CourseModel> FindById (int id);

        /// <summary>
        /// Search a course by its name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<CourseModel> FindByName (string name);

        /// <summary>
        /// Search courses by Department ID
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        Task<IEnumerable<CourseModel>> FindByDeptId (int deptId);

        /// <summary>
        /// Search courses by Department Name
        /// </summary>
        /// <param name="deptName"></param>
        /// <returns></returns>
        Task<IEnumerable<CourseModel>> FindByDeptName (string deptName);

        Task<IEnumerable<CourseModel>> FindByStudent (int studentId);
    }
}
