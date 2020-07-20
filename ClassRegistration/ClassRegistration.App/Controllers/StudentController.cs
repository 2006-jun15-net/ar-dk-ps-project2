using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassRegistration.Domain;
using ClassRegistration.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ClassRegistration.App.Controllers {

    [Route ("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase {

        private readonly IStudentRepository _studentRepository;

        public StudentController (IStudentRepository studentRepository) {
            _studentRepository = studentRepository;
        }

        // POST api/<EnrollmentController>/5/enrollment
        [HttpPost ("{id}/enrollment")]
        public async Task<IActionResult> PostEnrollment (int id, [FromBody] EnrollmentModel enrollment) {

            // TODO verify student and section by ids

            await _studentRepository.AddEnrollment (id, enrollment);

            return Ok ();
        }
    }
}
