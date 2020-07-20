
using ClassRegistration.DataAccess.Entity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography.X509Certificates;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ClassRegistration.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly Course_registration_dbContext _dbContext;

        public EnrollmentController(Course_registration_dbContext dbContext)
        {

            _dbContext = dbContext;

            

        }
        
       

        /// <summary>
        /// This method returns the total credits of a student with a specified ID and the term
        /// </summary>
        /// <param name="id"></param>
        /// <param name="term"></param>
        /// <returns></returns>

        // GET api/<EnrollmentController>/id
        [HttpGet("{id}/{term}")]
        public Object GetTotalCredits(int id,string term)
        {
            //return Ok(_enrollmentRepo.GetTotalCredits(id));

            int FallMinimumCredits = 20;  //fall minimum credits
            int WinterMinimumCredits = 8;  //winter minimum credits
            int SummerMinCredits = 8;       //summer minimum credits

            var totalCredits = (from c in _dbContext.Course
                         join s in _dbContext.Section on c.CourseId
                         equals s.CourseId
                         join e in _dbContext.Enrollment on s.SectId equals e.SectId
                         where e.StudentId == id && s.Term == term   //enrollments of a particular student with their respective semester.

                                select c.Credits).Sum();
            if(totalCredits >= FallMinimumCredits && term == "Fall")
            {
                return Ok(totalCredits);

            }
            else if(totalCredits >= WinterMinimumCredits && term == "Winter" )
            {
                return Ok(totalCredits);


            }

            else if(totalCredits >= SummerMinCredits && term == "Summer")
            {
                return Ok(totalCredits);


            }

            return NotFound();




        }



       
    }
}
