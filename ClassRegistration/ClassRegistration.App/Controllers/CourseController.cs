using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ClassRegistration.DataAccess.Entity;
using ClassRegistration.DataAccess.Repositories;
using ClassRegistration.DataAccess.Interfaces;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ClassRegistration.App.Controllers
{
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {

        
        private readonly ICourseRepository _courseRepo;
        


        public CourseController(ICourseRepository courserepo)
        {
            
            _courseRepo = courserepo;
            
        }
         
        //get all the courses available
        // GET: api/course/classes
        [HttpGet("classes")]
        public async Task<IActionResult> GetAllCoursesAvailable()
        {
            
            var theClasses = await _courseRepo.GetTheCourses();
            return Ok(theClasses);
        }



        //search a course by ID
        // GET api/course/class/100
        [HttpGet("class/{id}")]
        public async Task<ActionResult<Course>> GetCourseByCourseID(int id)
        {
            var theCourse = await _courseRepo.GetCourseByID(id);
            //if (_dbContext.Course.FirstOrDefault(c => c.CourseId == id) is Course theClass)
            if (theCourse is Domain.Model.Course theClass)
            { 
                return Ok(theClass);
            }
            return NotFound();
        }



        //get a course by its name
        // GET api/course/course/Robotics
        [HttpGet("course/{search}")]
        public async Task<ActionResult<Course>> GetCourseByCourseName(string search)
        {
            var theCourse = await _courseRepo.GetCourseByName(search);
            if (theCourse is Domain.Model.Course theClass)
            {
                return Ok(theClass);
            }
            return NotFound();
        }
         


        //search for courses available by Department ID
        // GET api/course/courses/1500
        [HttpGet("courses/{id}")]
        public async Task<ActionResult<Course>> GetCourseByDepartmentID(int id)
        {
            var theCourses = await _courseRepo.GetCourseByDepID(id);

            if (!theCourses.Any())
            {
                return NotFound();
            }
            return Ok(theCourses); 

        }


        

    }
}
