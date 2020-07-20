using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassRegistration.Domain;
using ClassRegistration.Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace ClassRegistration.App.Controllers {

    [Route ("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase {

        private readonly ICourseRepository _courseRepository;

        public StudentController (ICourseRepository courseRepository) {
            _courseRepository = courseRepository;
        }

        // GET api/<StudentController>/5
        [HttpGet ("{id}/courses")]
        public async Task<IEnumerable<CourseModel>> Get (int id) {
            return await _courseRepository.FindByStudent (id);
        }
    }
}
