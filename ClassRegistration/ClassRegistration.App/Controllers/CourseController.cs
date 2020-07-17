using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ClassRegistration.DataAccess.Entity;
using ClassRegistration.Domain.Repositories;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ClassRegistration.App.Controllers
{
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {

        private readonly Course_registration_dbContext _dbContext;
        private readonly CourseRepository _courseRepo;


        public CourseController(Course_registration_dbContext thecontext, CourseRepository _courserepo)
        {
            _dbContext = thecontext;
            _courseRepo = _courserepo;
        }
         
        //get all the courses available
        // GET: api/course/classes
        [HttpGet("classes")]
        public IActionResult GetAllCoursesAvailable()
        {
            //return Ok(_dbContext.Course.ToList());
            var theClasses = _courseRepo.GetTheCourses();
            return Ok(theClasses);
        }


        //get a course by ID
        // GET api/course/classes/100
        [HttpGet("classes/{id}")]
        public ActionResult<Course> GetCourseByID(int id)
        {
            var theCourse = _courseRepo.GetCourseByID(id);
            //if (_dbContext.Course.FirstOrDefault(c => c.CourseId == id) is Course theClass)
            if (theCourse is Course theClass)
            { 
                return theClass;
            }
            return NotFound();
        }



        // POST api/values
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        // PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
