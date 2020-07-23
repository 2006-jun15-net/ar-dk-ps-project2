using ClassRegistration.DataAccess.Interfaces;
using ClassRegistration.Domain;
using Microsoft.AspNetCore.Mvc;
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

        public StudentController (ICourseRepository courseRepository,
                                    IStudentRepository studentRepository, IEnrollmentRepository enrollmentRepository)
        {
            _courseRepository = courseRepository;
            _studentRepository = studentRepository;
            _enrollmentRepository = enrollmentRepository;
        }

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

            if (courses.Count () == 0)
            {
                return NoContent ();
            }

            return Ok (courses);
        }

        /// <summary>
        /// This method gets the total amount of registred courses.
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

            return Ok (totalAmount);
        }

        [HttpGet ("{id}/credits_met")]
        public async Task<IActionResult> GetCreditsRequirementsMet (int id)
        {
            var student = await _studentRepository.FindById (id);

            if (student == default)
            {
                return BadRequest ();
            }

            return Ok (new { requirmentsMet = student.CreditRequirementsMet });
        }
    }
}
