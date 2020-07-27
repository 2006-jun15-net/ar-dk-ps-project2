using ClassRegistration.App.ResponseObjects;
using ClassRegistration.DataAccess.Interfaces;
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
    public class SectionController : ControllerBase
    {
        private readonly ISectionRepository _sectionRepository;
        private readonly ICourseRepository _courseRepository;

        public SectionController (ISectionRepository sectionRepository,
                                    ICourseRepository courseRepository)
        {
            _sectionRepository = sectionRepository;
            _courseRepository = courseRepository;
        }

        /// <summary>
        /// Get all the sections with matching instructor, if specified
        /// <param name="instructorId"></param>
        /// <returns></returns>
        // GET: api/section?instructorId=5
        [HttpGet]
        public async Task<IActionResult> Get ([FromQuery] int instructorId)
        {
            IEnumerable<SectionModel> sections;

            sections = await _sectionRepository.FindByInstrId (Convert.ToInt32 (instructorId));

            if (!sections.Any ())
            {
                return NoContent ();
            }

            return Ok (sections);
        }

        [HttpPost]
        [Authorize (Policy = "AdminAccess")]
        public async Task<IActionResult> Post ([FromBody] SectionModel section)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest (new ErrorObject ("Invalid data sent"));
            }

            CourseModel course;

            try
            {
                course = await _courseRepository.FindById (section.CourseId);
            }
            catch (ArgumentException e)
            {
                return BadRequest (new ValidationError (e));
            }

            if (course == default)
            {
                return BadRequest (new ErrorObject ($"Course id {section.CourseId} does not exist"));
            }

            var success = await _sectionRepository.Add (section.InstructorId, section.CourseId, section.Term, section.StartTime, section.EndTime);

            if (!success)
            {
                return BadRequest (new ErrorObject ("Failed to add course"));
            }

            return Ok (MessageObject.Success);
        }
    }
}
