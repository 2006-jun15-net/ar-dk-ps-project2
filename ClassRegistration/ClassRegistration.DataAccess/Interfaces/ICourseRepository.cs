using ClassRegistration.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClassRegistration.DataAccess.Interfaces
{
    public interface ICourseRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        Task<IEnumerable<CourseModel>> FindByStudent (int studentId);

        /// <summary>
        /// get all the courses available
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<CourseModel>> FindAll ();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<CourseModel> FindById (int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<CourseModel> FindByName (string name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        Task<IEnumerable<CourseModel>> FindByDeptID (int deptId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="deptName"></param>
        /// <returns></returns>
        Task<IEnumerable<CourseModel>> FindByDeptName (string deptName);
    }
}
