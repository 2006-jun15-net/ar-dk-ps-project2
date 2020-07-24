using ClassRegistration.DataAccess.Interfaces;
using ClassRegistration.Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ClassRegistration.App.Controllers
{
    [Route ("api/[controller]")]
    [ApiController]
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

        /// <summary>
        /// Search for a student by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET api/<StudentController>/5
        [HttpGet ("{id}")]
        public async Task<IActionResult> Get (int id)
        {
            var student = await _studentRepository.FindById (id);

            if (student == default)
            {
                return NotFound ();
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
            var student = await _studentRepository.FindById (id);

            if (student == default)
            {
                return NotFound ();
            }

            var courses = await _courseRepository.FindByStudent (id);

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
        // GET api/<StudentController>/1/Fall
        [HttpGet ("{id}/{term}")]
        public async Task<IActionResult> GetTotalAmount (int id, string term)
        {
            decimal? totalAmount = await _enrollmentRepository.GetTotalAmount (id, term); //getting the amount owed by a student in a particular semester.

            if (totalAmount == null)
            {
                return BadRequest ();
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
            var student = await _studentRepository.FindById (id);

            if (student == default)
            {
                return BadRequest ();
            }

            var discount = await _studentTypeRepository.FindDiscount (student.ResidentId);

            return Ok (discount);
        }
    }
}
