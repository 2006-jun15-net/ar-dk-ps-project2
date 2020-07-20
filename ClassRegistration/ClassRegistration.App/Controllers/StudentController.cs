using ClassRegistration.Domain;
using ClassRegistration.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClassRegistration.App.Controllers {

    [Route ("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase {

        private readonly ICourseRepository _courseRepository;
        private readonly IStudentRepository _studentRepository;

        public StudentController (ICourseRepository courseRepository,
                                    IStudentRepository studentRepository) {

            _courseRepository = courseRepository;
            _studentRepository = studentRepository;
        }

        // GET api/<StudentController>/5
        [HttpGet ("{id}")]
        public async Task<StudentModel> Get (int id) {
            return await _studentRepository.FindById (id);
        }

        // GET api/<StudentController>/5/courses
        [HttpGet ("{id}/courses")]
        public async Task<IEnumerable<CourseModel>> GetCourses (int id) {



            return await _courseRepository.FindByStudent (id);
        }
    }
}
