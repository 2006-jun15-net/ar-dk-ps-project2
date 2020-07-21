

using ClassRegistration.DataAccess.Repositories;
using ClassRegistration.DataAccess.Entity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography.X509Certificates;
using ClassRegistration.DataAccess;
using ClassRegistration.DataAccess.Interfaces;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ClassRegistration.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {

        /// <summary>
        /// private IEnrollment field.
        /// </summary>
        private IEnrollmentRepository _enrollmentRepository;

        
        /// <summary>
        /// Constructor initializes IEnrollment field.
        /// </summary>
        /// <param name="enrollmentRepo"></param>
        public EnrollmentController(IEnrollmentRepository enrollmentRepo)
        {
           _enrollmentRepository = enrollmentRepo;

        }
        
       

        /// <summary>
        /// This method returns the total credits of a student with a specified ID and the term
        /// </summary>
        /// <param name="id"></param>
        /// <param name="term"></param>
        /// <returns></returns>

        // GET api/<EnrollmentController>/id
        [HttpGet("{id}/{term}")]
        public IActionResult GetTotalCredits(int id,string term)
        {
            //return Ok(_enrollmentRepo.GetTotalCredits(id));

            int FallMinimumCredits = 20;  //fall minimum credits
            int WinterMinimumCredits = 8;  //winter minimum credits
            int SummerMinCredits = 8;       //summer minimum credits


            int totalCredits = _enrollmentRepository.GetCredits(id, term); //gets total credits of a student with an id and term they are enrolled.

            string[] semesters = { "Fall", "Winter", "Summer" };  // an array of semesters for validation
            foreach(var semester in semesters)
            {
                if(semester == term)
                {
                    if (totalCredits >= FallMinimumCredits && term == "Fall")
                    {
                        return Ok(totalCredits);

                    }
                    else if (totalCredits >= WinterMinimumCredits && term == "Winter")
                    {
                        return Ok(totalCredits);


                    }

                    else if (totalCredits >= SummerMinCredits && term == "Summer")
                    {
                        return Ok(totalCredits);


                    }

                }
            }
          
            return NotFound();




        }



       
    }
}
