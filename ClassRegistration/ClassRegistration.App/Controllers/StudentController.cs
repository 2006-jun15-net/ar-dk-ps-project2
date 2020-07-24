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

        /// <summary>
        /// Gets the total amount of fees a student needs to pay in a semester after a discount is applied
        /// </summary>
        /// <param name="id"></param>
        /// <param name="term"></param>
        /// <param name="resident_id"></param>
        /// <returns></returns>

        //GET api/<StudentController>/1//Fall/In-state
        [HttpGet("{id}/{term}/{resident_id}")]
        public async Task<IActionResult> GetFinalAmount(int id, string term, string resident_id)
        {
            decimal? finalAmount = await _enrollmentRepository.FinalAmountDiscounted(id, term, resident_id);

            if(finalAmount == null)
            {
                return BadRequest();
            }

            return Ok(finalAmount);
        }
    }
}
