using ClassRegistration.App.ResponseObjects;
using ClassRegistration.DataAccess.Interfaces;
using ClassRegistration.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
        // GET api/course/100
        [HttpGet ("{id}")]
        public async Task<IActionResult> Get (int id)
        {
            CourseModel theCourse;

            try
            {
                theCourse = await _courseRepository.FindById (id);
            }
            catch (ArgumentException e)
            {
                return BadRequest (new ValidationError (e));
            }

            if (theCourse is CourseModel theClass)
            {
                return Ok (theClass);
            }

            return NotFound (new ErrorObject ($"Course id {id} does not exist"));
        }

        /// <summary>
        /// search a course by its name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        // GET api/course/Robotics
        [HttpGet ("{name}")]
        public async Task<IActionResult> Get (string name)
        {
            CourseModel theCourse;

            try
            {
                theCourse = await _courseRepository.FindByName (name);
            }
            catch (ArgumentException e)
            {
                return BadRequest (new ValidationError (e));
            }

            if (theCourse is CourseModel theClass)
            {
                return Ok (theClass);
            }

            return NotFound (new ErrorObject ($"Course '{name}' does not exist"));
        }

        /// <summary>
        /// search for courses available in a department
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        // GET api/courses?deptId=5
        [HttpGet]
        public async Task<IActionResult> GetByDepartmentId ([FromBody] int deptId)
        {
            IEnumerable<CourseModel> theCourses;

            try
            {
                theCourses = await _courseRepository.FindByDeptId (deptId);
            }
            catch (ArgumentException e)
            {
                return BadRequest (new ValidationError (e));
            }

            if (!theCourses.Any ())
            {
                return NotFound (new ErrorObject ($"Department id {deptId} does not exist"));
            }

            return Ok (theCourses);
        }
    }
}
