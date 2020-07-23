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
            int? totalCredits = await _enrollmentRepository.GetCredits (id, term);  // gets total credits of a student with an id and term they are enrolled.

            if (totalCredits == null)
            {
                return BadRequest ();
            }

            var minimumCredits = EnrollmentModel.MinimumCredits (term);

            if (minimumCredits == -1)
            {
                return BadRequest ();
            }

            return Ok (new { requirementsMet = totalCredits >= minimumCredits });
        }

        // POST api/<EnrollmentController>
        //[HttpPost]
        //public async Task<IActionResult> Post ([FromBody] EnrollmentModel enrollmentModel)
        //{
        //    // TODO check that section exists

        //    if (_studentRepository.FindById (enrollmentModel.StudentId) == default)
        //    {
        //        return BadRequest ();
        //    }

        //    bool success = await _enrollmentRepository.Add (enrollmentModel);

        //    if (!success)
        //    {
        //        return BadRequest ();
        //    }

        //    return Ok ();
        //}
    }
}