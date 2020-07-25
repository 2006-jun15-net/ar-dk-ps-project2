using ClassRegistration.App.ResponseObjects;
using ClassRegistration.DataAccess.Interfaces;
using ClassRegistration.Domain;
using ClassRegistration.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ClassRegistration.App.Controllers
{
    [Route ("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ISectionRepository _sectionRepository;

        /// <summary>
        /// Constructor initializes IEnrollment field.
        /// </summary>
        /// <param name="enrollmentRepository"></param>
        /// <param name="studentRepository"></param>
        public EnrollmentController (IEnrollmentRepository enrollmentRepository,
                                     IStudentRepository studentRepository,
                                     ISectionRepository sectionRepository)
        {
            _enrollmentRepository = enrollmentRepository;
            _studentRepository = studentRepository;
            _sectionRepository = sectionRepository;
        }

        /// <summary>
        /// Remove course from registered courses
        /// </summary>
        /// <param name="id"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        // DELETE api/<EnrollmentController>/5?studentId=5
        [HttpDelete ("{id}")]
        public async Task<IActionResult> Delete (int id, [FromBody] int studentId)
        {
            StudentModel student;

            try
            {
                student = await _studentRepository.FindById (studentId);
            }
            catch (ArgumentException e)
            {
                return BadRequest (new ValidationError (e));
            }

            if (student == default)
            {
                return BadRequest (new ErrorObject ($"Student id {studentId} does not exist"));
            }

            bool deleted = await _enrollmentRepository.Delete (student.StudentId, id);

            if (!deleted)
            {
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
            int? totalCredits = await _enrollmentRepository.GetCredits (id, term);  // gets total credits of a student with an id and term they are enrolled.

            if (totalCredits == null)
            {
                return BadRequest (new ErrorObject ($"Couldn't find total credits for student id {id} and term {term}"));
            }

            var minimumCredits = EnrollmentModel.MinimumCredits (term);

            if (minimumCredits == -1)
            {
                return BadRequest (new ErrorObject ($"Invalid term {term}"));
            }

            return Ok (totalCredits >= minimumCredits);
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

            await _enrollmentRepository.Add (enrollmentModel.StudentId, enrollmentModel.SectId);

            return Ok (MessageObject.Success);
        }
    }
}