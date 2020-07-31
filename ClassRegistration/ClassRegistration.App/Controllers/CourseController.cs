using ClassRegistration.App.ResponseObjects;
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
        public async Task<IActionResult> Get ([FromQuery] ModelPagination modelPagination, [FromQuery] string courseName)
        {
            if (!ModelState.IsValid)
            {
                //Logging invalid input.
                if (_logger != null)
                {
                    _logger.LogError ("There was some invalid data");
                }

                return BadRequest (new ErrorObject ("Invalid data sent"));
            }

            IEnumerable<CourseModel> courses;

            if (!string.IsNullOrEmpty (courseName))
            {
                CourseModel theCourse;

                try
                {
                    if (_logger != null)
                    {
                        _logger.LogDebug ($"Getting a course by name, {courseName}");
                    }
                    theCourse = await _courseRepository.FindByName (courseName);
                }
                catch (ArgumentException e)
                {
                    return BadRequest (new ValidationError (e));
                }

                if (theCourse == default)
                {
                    return NotFound (new ErrorObject ($"Course '{courseName}' does not exist"));
                }

                courses = new List<CourseModel> ();
                courses.Append (theCourse);
            }
            else
            {
                //Waiting to recieve some courses.
                if (_logger != null)
                {
                    _logger.LogDebug ("Waiting to retrieve courses...");
                }
                courses = await _courseRepository.FindAll (modelPagination);
            }

            if (!courses.Any ())
            {
                //log information warning for no courses
                if (_logger != null)
                {
                    _logger.LogWarning ("There are no courses");
                }
                return NoContent ();
            }

            //log information courses have been received
            if (_logger != null)
            {
                _logger.LogInformation ("Retrieved courses");
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
                //waiting to get a course by an ID.
                if (_logger != null)
                {
                    _logger.LogDebug ("Getting a course by its ID executing...");
                }
                theCourse = await _courseRepository.FindById (id);
            }
            catch (ArgumentException e)
            {
                //logging information error for bad requests.
                if (_logger != null)
                {
                    _logger.LogError ("A bad request was made for the course");
                }
                return BadRequest (new ValidationError (e));
            }

            if (theCourse is CourseModel theClass)
            {
                //log information for when a course with a specific id is found
                if (_logger != null)
                {
                    _logger.LogInformation ($"Retrieved a course with ID:{id}");
                }
                return Ok (theClass);
            }

            // providing a warning if the course by the given id does not exist.
            if (_logger != null)
            {
                _logger.LogWarning ($"A course by name,{id} does not exist ");
            }

            return NotFound (new ErrorObject ($"Course id {id} does not exist"));
        }
    }
}
