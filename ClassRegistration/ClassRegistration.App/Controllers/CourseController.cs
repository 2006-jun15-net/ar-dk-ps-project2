using ClassRegistration.App.ResponseObjects;
using ClassRegistration.DataAccess.Interfaces;
using ClassRegistration.DataAccess.Pagination;
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
        /// get all the courses by department, if specified
        /// </summary>
        /// <returns></returns>
        // GET: api/course?PageNumber=1&PageSize=5 or api/course?deptId=5
        [HttpGet]
        public async Task<IActionResult> Get ([FromQuery] CoursePagination coursePagination, [FromQuery] int? deptId = null)
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest (new ErrorObject ("Invalid data sent"));
            }
            
            IEnumerable<CourseModel> courses;

            if (deptId != null) 
            {
                courses = await _courseRepository.FindByDeptId (Convert.ToInt32(deptId));
            }
            else
            {
                courses = await _courseRepository.FindAll (coursePagination);
            }

            if (!courses.Any ()) 
            {
                return NoContent ();
            }

            return Ok (courses);
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
    }
}
