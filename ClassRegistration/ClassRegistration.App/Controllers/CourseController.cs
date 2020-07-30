using ClassRegistration.App.ResponseObjects;
using ClassRegistration.DataAccess.Entity;
using ClassRegistration.DataAccess.Interfaces;
using ClassRegistration.DataAccess.Pagination;
using ClassRegistration.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<CourseController> _logger;

        public CourseController (ICourseRepository courseRepository, ILogger<CourseController> logger)
        {
            _courseRepository = courseRepository;
            _logger = logger;
        }

        /// <summary>
        /// get all the courses by department, if specified
        /// </summary>
        /// <returns></returns>
        // GET: api/course?PageNumber=1&PageSize=5 or api/course?deptId=5
        [HttpGet]
        public async Task<IActionResult> Get ([FromQuery] ModelPagination modelPagination, [FromQuery] int? deptId = null)
        {
            if (!ModelState.IsValid)
            {
                //Logging invalid input.
                _logger.LogError("There was some invalid data");
                return BadRequest (new ErrorObject ("Invalid data sent"));
            }

            IEnumerable<CourseModel> courses;

            if (deptId != null)
            {
                //waiting to recive courses by ID.
                _logger.LogDebug("Get by dept ID method executing...");
                courses = await _courseRepository.FindByDeptId (Convert.ToInt32 (deptId));
            }
            else
            {
                //Waiting to recieve some courses.
                _logger.LogDebug("Waiting to retrieve courses...");
                courses = await _courseRepository.FindAll (modelPagination);
            }

            if (!courses.Any ())
            {
                //log information warning for no courses
                _logger.LogWarning("There are no courses");
                return NoContent ();
            }

            //log information courses have been received
            _logger.LogInformation("Retrieved courses");
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
                //waiting to get a course by an ID.
                _logger.LogDebug("Getting a course by its ID executing...");
                theCourse = await _courseRepository.FindById (id);
            }
            catch (ArgumentException e)
            {
                //logging information error for bad requests.
                _logger.LogError("A bad request was made for the course");
                return BadRequest (new ValidationError (e));
            }

            if (theCourse is CourseModel theClass)
            {
                //log information for when a course with a specific id is found
                _logger.LogInformation($"Retrieved a course with ID:{id}");
                return Ok (theClass);
            }

            // providing a warning if the course by the given id does not exist.
            _logger.LogWarning($"A course by name,{id} does not exist ");

            return NotFound (new ErrorObject ($"Course id {id} does not exist"));
        }

        /// <summary>
        /// search a course by its name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        // GET api/course/class/Robotics
        [HttpGet ("class/{name}")]
        public async Task<IActionResult> Get (string name)
        {
            CourseModel theCourse;

            try
            {
                //debugging by the course with a given name.
                _logger.LogDebug($"Getting a course by name, {name}");
                theCourse = await _courseRepository.FindByName (name);
            }
            catch (ArgumentException e)
            {
                return BadRequest (new ValidationError (e));
            }

            if (theCourse is CourseModel theClass)
            {
                //log infromation for retrieved courses
                _logger.LogInformation($"Retrieved course with name,{name}");
                return Ok (theClass);
            }

            // providing a warning if the course by the given name does not exist.
            _logger.LogWarning($"A course by name,{name} does not exist ");

            return NotFound (new ErrorObject ($"Course '{name}' does not exist"));
        }
    }
}
