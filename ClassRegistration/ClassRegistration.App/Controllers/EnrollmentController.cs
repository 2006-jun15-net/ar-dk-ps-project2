using ClassRegistration.App.ResponseObjects;
using ClassRegistration.DataAccess.Interfaces;
using ClassRegistration.Domain;
using ClassRegistration.Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ClassRegistration.App.Controllers
{
    [Route ("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EnrollmentController : ControllerBase
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ISectionRepository _sectionRepository;
        private readonly ILogger<EnrollmentController> _logger;

        /// <summary>
        /// Constructor initializes IEnrollment field.
        /// </summary>
        /// <param name="enrollmentRepository"></param>
        /// <param name="studentRepository"></param>
        public EnrollmentController (IEnrollmentRepository enrollmentRepository,
                                     IStudentRepository studentRepository,
                                     ISectionRepository sectionRepository, ILogger<EnrollmentController> logger)
        {
            _enrollmentRepository = enrollmentRepository;
            _studentRepository = studentRepository;
            _sectionRepository = sectionRepository;
            _logger = logger;
        }

        /// <summary>
        /// Remove course from registered courses
        /// </summary>
        /// <param name="id"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        // DELETE api/<EnrollmentController>/5?studentId=5
        [HttpDelete ("{id}")]
        public async Task<IActionResult> Delete (int id, [FromQuery] int studentId)
        {
            StudentModel student;

            try
            {
                //logging information for awaiting to retrive a student
                _logger.LogDebug($"Retrieving a student with ID:{id}");
                student = await _studentRepository.FindById (studentId);
            }
            catch (ArgumentException e)
            {
                _logger.LogError("Invalid enrollment request");
                return BadRequest (new ValidationError (e));
            }

            if (student == default)
            {
                _logger.LogWarning($"student with ID:{studentId}");
                return BadRequest (new ErrorObject ($"Student id {studentId} does not exist"));
            }

            _logger.LogInformation($"Awaiting to delete a student enrollment with id, {id}");
            bool deleted = await _enrollmentRepository.Delete (id, student.StudentId);

            if (!deleted)
            {
                _logger.LogWarning($"student enrollment with id: {id}");
                return NotFound (new ErrorObject ($"Student enrollment id {id} does not exist"));
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
            _logger.LogDebug($"Awaiting to get total credits for student with id, {id} in {term}");
            int? totalCredits = await _enrollmentRepository.GetCredits (id, term);  // gets total credits of a student with an id and term they are enrolled.

            if (totalCredits == null)
            {
                _logger.LogWarning("The registered data does not exist.");
                return BadRequest (new ErrorObject ($"Couldn't find total credits for student id {id} and term {term}"));
            }
            _logger.LogInformation($"Retrieved total credits for student with id, {id} in {term}");
            return Ok (totalCredits);
        }

        /// <summary>
        /// Register for a course
        /// </summary>
        /// <param name="enrollmentModel"></param>
        /// <returns></returns>
        // POST api/<EnrollmentController>
        [HttpPost]
        public async Task<IActionResult> Post ([FromBody] EnrollmentModel enrollmentModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest (new ErrorObject ("Invalid enrollment data sent"));
            }

            SectionModel section;
            StudentModel student;

            try
            {
                section = await _sectionRepository.FindById (enrollmentModel.SectId);
                student = await _studentRepository.FindById (enrollmentModel.StudentId);
            }
            catch (ArgumentException e)
            {
                return BadRequest (new ValidationError (e));
            }

            if (student == default)
            {
                return BadRequest (new ErrorObject ($"Student id {enrollmentModel.StudentId} does not exist"));
            }

            if (section == default)
            {
                return BadRequest (new ErrorObject ($"Section id {enrollmentModel.SectId} does not exist"));
            }

            var success = await _enrollmentRepository.Add (enrollmentModel.StudentId, enrollmentModel.SectId);

            if (!success)
            {
                return BadRequest (new ErrorObject ("Failed to add enrollment"));
            }

            return Ok (MessageObject.Success);
        }
    }
}