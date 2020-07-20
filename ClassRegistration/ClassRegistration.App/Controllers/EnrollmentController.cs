using ClassRegistration.Domain;
using ClassRegistration.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace ClassRegistration.App.Controllers {

    [Route ("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase {

        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IStudentRepository _studentRepository;

        public EnrollmentController (IEnrollmentRepository enrollmentRepository,
                                     IStudentRepository studentRepository) {

            _enrollmentRepository = enrollmentRepository;
            _studentRepository = studentRepository;
        }

        // DELETE api/<EnrollmentController>/5
        [HttpDelete ("{id}")]
        public async Task<IActionResult> DeleteFromStudent (int id, [FromBody] int studentId) {

            var student = _studentRepository.FindById (studentId);

            if (student == default) {
                return BadRequest ();
            }

            bool deleted = await _enrollmentRepository.Delete (student.Id, id);

            if (!deleted) {
                return NotFound ();
            }

            return Ok ();
        }
    }
}
