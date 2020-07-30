using ClassRegistration.App.ResponseObjects;
using ClassRegistration.DataAccess.Interfaces;
using ClassRegistration.Domain.Model;
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

        public SectionController (ISectionRepository sectionRepository)
        {
            _sectionRepository = sectionRepository;
        }

        /// <summary>
        /// Get all the sections with matching instructor, if specified
        /// <param name="courseId"></param>
        /// <returns></returns>
        // GET: api/section?courseId=5
        [HttpGet]
        public async Task<IActionResult> Get ([FromQuery] int courseId)
        {
            SectionModel section;

            try
            {
                section = await _sectionRepository.FindByCourseId(courseId);
            }
            catch(ArgumentException e)
            {
                return BadRequest (new ValidationError (e));
            }

            return Ok (section);
        }

        // using instructor's last name here
        // GET: api/section/instructor/{Erickson}
        [HttpGet ("instructor/{instructorName}")]
        public async Task<IActionResult> GetByInstructor (string instructorName)
        {
            IEnumerable<SectionModel> sections;

            sections = await _sectionRepository.FindByInstrName (instructorName);

            if (!sections.Any ())
            {
                return NoContent ();
            }

            return Ok (sections);
        }

        [HttpGet ("class/{courseName}")]
        public async Task<IActionResult> GetByCourse (string courseName)
        {
            IEnumerable<SectionModel> sections;

            sections = await _sectionRepository.FindByCourseName (courseName);

            if (!sections.Any ())
            {
                return NoContent ();
            }

            return Ok (sections);
        }
    }
}
