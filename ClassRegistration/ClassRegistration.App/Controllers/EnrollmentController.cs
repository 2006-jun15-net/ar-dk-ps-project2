using ClassRegistration.DataAccess.Interfaces;
using ClassRegistration.Domain;
using ClassRegistration.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ClassRegistration.App.Controllers
{
    [Route ("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IStudentRepository _studentRepository;

        /// <summary>
        /// Constructor initializes IEnrollment field.
        /// </summary>
        /// <param name="enrollmentRepository"></param>
        /// <param name="studentRepository"></param>
        public EnrollmentController (IEnrollmentRepository enrollmentRepository,
                                     IStudentRepository studentRepository)
        {
            _enrollmentRepository = enrollmentRepository;
            _studentRepository = studentRepository;
        }

        // DELETE api/<EnrollmentController>/id
        [HttpDelete ("{id}")]
        public async Task<IActionResult> Delete (int id, [FromBody] int studentId)
        {
            var student = _studentRepository.FindById (studentId);

            if (student == default)
            {
                return BadRequest ();
            }

            bool deleted = await _enrollmentRepository.Delete (student.Id, id);

            if (!deleted)
            {
                return NotFound ();
            }

            return Ok ();
        }

        /// <summary>
        /// This method returns the total credits of a student with a specified ID and the term
        /// </summary>
        /// <param name="id"></param>
        /// <param name="term"></param>
        /// <returns></returns>

        // GET api/<EnrollmentController>/id/term
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

        // POST api/<EnrollmentController>
        [HttpPost]
        public async Task<IActionResult> Post ([FromBody] EnrollmentModel enrollmentModel)
        {
            // TODO check that section exists

            if (_studentRepository.FindById (enrollmentModel.StudentId) == default)
            {
                return BadRequest ();
            }

            bool success = await _enrollmentRepository.Add (enrollmentModel);

            if (!success)
            {
                return BadRequest ();
            }

            return Ok ();
        }
    }
}