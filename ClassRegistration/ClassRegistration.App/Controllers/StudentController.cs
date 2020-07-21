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

        public StudentController (ICourseRepository courseRepository,
                                    IStudentRepository studentRepository)
        {
            _courseRepository = courseRepository;
            _studentRepository = studentRepository;
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
    }
}
