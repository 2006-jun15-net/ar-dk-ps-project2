using ClassRegistration.DataAccess.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ClassRegistration.App.Controllers
{
    [Route ("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        /// <summary>
        /// private IEnrollment field.
        /// </summary>
        private readonly IEnrollmentRepository _enrollmentRepository;

        /// <summary>
        /// Constructor initializes IEnrollment field.
        /// </summary>
        /// <param name="enrollmentRepo"></param>
        public EnrollmentController (IEnrollmentRepository enrollmentRepository)
        {
            _enrollmentRepository = enrollmentRepository;
        }

        /// <summary>
        /// This method returns the total credits of a student with a specified ID and the term
        /// </summary>
        /// <param name="id"></param>
        /// <param name="term"></param>
        /// <returns></returns>

        // GET api/<EnrollmentController>/id
        [HttpGet ("{id}/{term}")]
        public async Task<IActionResult> GetTotalCredits (int id, string term)
        {
            //return Ok(_enrollmentRepo.GetTotalCredits(id));

            int FallMinimumCredits = 20;  //fall minimum credits
            int WinterMinimumCredits = 8;  //winter minimum credits
            int SummerMinCredits = 8;       //summer minimum credits


            int? totalCredits = await _enrollmentRepository.GetCredits (id, term);  // gets total credits of a student with an id and term they are enrolled.

            if (totalCredits == null)
            {
                // A bad thing happened
            }

            string[] semesters = { "Fall", "Winter", "Summer" };  // an array of semesters for validation
            foreach (var semester in semesters)
            {
                if (semester == term)
                {
                    if (totalCredits >= FallMinimumCredits && term == "Fall")
                    {
                        return Ok (totalCredits);
                    }

                    else if (totalCredits >= WinterMinimumCredits && term == "Winter")
                    {
                        return Ok (totalCredits);
                    }

                    else if (totalCredits >= SummerMinCredits && term == "Summer")
                    {
                        return Ok (totalCredits);
                    }

                    else
                    {
                        // Something bad happened, return a response to reflect that
                    }
                }
            }

            return NotFound ();
        }
    }
}
