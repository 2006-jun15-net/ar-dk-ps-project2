using ClassRegistration.App.ResponseObjects;
using ClassRegistration.DataAccess.Interfaces;
using ClassRegistration.Domain;
using ClassRegistration.Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassRegistration.App.Controllers
{
    [Route ("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StudentController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IStudentTypeRepository _studentTypeRepository;

        public StudentController (ICourseRepository courseRepository,
                                    IStudentRepository studentRepository,
                                    IEnrollmentRepository enrollmentRepository,
                                    IStudentTypeRepository studentTypeRepository)
        {
            _courseRepository = courseRepository;
            _studentRepository = studentRepository;
            _enrollmentRepository = enrollmentRepository;
            _studentTypeRepository = studentTypeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get ([FromQuery] string FirstName, [FromQuery] string LastName)
        {
            var UserID = User.Identity.Name;

            if (string.IsNullOrEmpty (FirstName) && string.IsNullOrEmpty (LastName))
            {
                return BadRequest (new ErrorObject ("This method requires a firstname or a lastname to be provided"));
            }

            if (string.IsNullOrEmpty (FirstName))
            {
                var students = await _studentRepository.FindByLastname (LastName);

                if (!students.Any ())
                {
                    return NoContent ();
                }

                return Ok (students);
            }

            else if (string.IsNullOrEmpty (LastName))
            {
                var students = await _studentRepository.FindByFirstname (FirstName);

                if (!students.Any ())
                {
                    return NoContent ();
                }

                return Ok (students);
            }

            else
            {
                var student = await _studentRepository.FindByName (FirstName, LastName);
                return Ok (student);
            }
        }

        /// <summary>
        /// Search for a student by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET api/<StudentController>/5
        [HttpGet ("{id}")]
        public async Task<IActionResult> Get (int id)
        {
            foreach (var identity in HttpContext.User.Identities)
            {
                Console.WriteLine (identity.Name);
            }

            StudentModel student;

            try
            {
                student = await _studentRepository.FindById (id);
            }
            catch (ArgumentException e)
            {
                return BadRequest (new ValidationError (e));
            }

            if (student == default)
            {
                return NotFound (new ErrorObject ($"Student id {id} does not exist"));
            }

            return Ok (student);
        }

        /// <summary>
        /// Returns a student's courses
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET api/<StudentController>/5/courses
        [HttpGet ("{id}/courses")]
        public async Task<IActionResult> GetCourses (int id)
        {
            StudentModel student;

            try
            {
                student = await _studentRepository.FindById (id);
            }
            catch (ArgumentException e)
            {
                return BadRequest (new ValidationError (e));
            }

            if (student == default)
            {
                return NotFound (new ErrorObject ($"Student id {id} does not exist"));
            }

            IEnumerable<CourseModel> courses;

            try
            {
                courses = await _courseRepository.FindByStudent (id);
            }
            catch (ArgumentException e)
            {
                return BadRequest (new ValidationError (e));
            }

            if (!courses.Any ())
            {
                return NoContent ();
            }

            return Ok (courses);
        }

        /// <summary>
        /// This method gets the total amount a student owes for their registered courses
        /// </summary>
        /// <param name="id"></param>
        /// <param name="term"></param>
        /// <returns></returns>
        // GET api/<StudentController>/1/fall
        [HttpGet ("{id}/{term}")]
        public async Task<IActionResult> GetTotalAmount (int id, string term)
        {
            StudentModel student;

            try
            {
                student = await _studentRepository.FindById (id);
            }
            catch (ArgumentException e)
            {
                return BadRequest (new ValidationError (e));
            }

            if (student == default)
            {
                return NotFound (new ErrorObject ($"Student id {id} does not exist"));
            }

            decimal? totalAmount = await _enrollmentRepository.GetTotalAmount (id, term); //getting the amount owed by a student in a particular semester.

            if (totalAmount == null)
            {
                return BadRequest (new ErrorObject ($"Unable to find total amount for student id {id} and term {term}"));
            }

            return Ok (Convert.ToDecimal (totalAmount));
        }

        /// <summary>
        /// Gets the student's discount based on whether or not they are a resident
        /// </summary>
        /// <param name="id"></param>
        /// <param name="term"></param>
        /// <param name="residentId"></param>
        /// <returns></returns>
        //GET api/<StudentController>/1/discount
        [HttpGet ("{id}/discount")]
        public async Task<IActionResult> GetDiscount (int id)
        {
            StudentModel student;

            try
            {
                student = await _studentRepository.FindById (id);
            }
            catch (ArgumentException e)
            {
                return BadRequest (new ValidationError (e));
            }

            if (student == default)
            {
                return NotFound (new ErrorObject ("Student id {id} does not exist"));
            }

            var discount = await _studentTypeRepository.FindDiscount (student.ResidentId);

            return Ok (discount);
        }
    }
}
