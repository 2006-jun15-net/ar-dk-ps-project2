using ClassRegistration.DataAccess.Interfaces;
using ClassRegistration.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace ClassRegistration.App.Controllers
{
    [Route ("api/[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;

        public CourseController (ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        /// <summary>
        /// get all the courses available
        /// </summary>
        /// <returns></returns>
        // GET: api/course
        [HttpGet]
        public async Task<IActionResult> Get ()
        {
            var theClasses = await _courseRepository.FindAll ();
            return Ok (theClasses);
        }

        /// <summary>
        /// search a course by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET api/course/class/100
        [HttpGet ("class/{id}")]
        public async Task<ActionResult<CourseModel>> Get (int id)
        {
            var theCourse = await _courseRepository.FindById (id);

            if (theCourse is CourseModel theClass)
            {
                return Ok (theClass);
            }
            return NotFound ();
        }


        /// <summary>
        /// search a course by its name
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        // GET api/course/Robotics
        [HttpGet ("course/{search}")]
        public async Task<ActionResult<CourseModel>> GetByCourseName (string search)
        {
            var theCourse = await _courseRepository.FindByName (search);

            if (theCourse is CourseModel theClass)
            {
                return Ok (theClass);
            }
            return NotFound ();
        }



        /// <summary>
        /// search for courses available in a department
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        // GET api/courses/1500
        [HttpGet ("courses/{deptId}")]
        public async Task<ActionResult<CourseModel>> GetByDepartmentID (int deptId)
        {
            var theCourses = await _courseRepository.FindByDeptID (deptId);

            if (!theCourses.Any ())
            {
                return NotFound ();
            }
            return Ok (theCourses);
        }
    }
}
