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

        // using instructor's last name here
        // GET: api/section/{Erickson}
        [HttpGet ("{instructorName}")]
        public async Task<IActionResult> GetCoursesAndReviews (string instructorName)
        {
            IEnumerable<SectionModel> sections;

            if (!string.IsNullOrEmpty (instructorName))
            {
                sections = await _sectionRepository.FindByInstrName (instructorName);
            }
            else
            {
                sections = await _sectionRepository.FindAll ();
            }

            if (!sections.Any ())
            {
                return NoContent ();
            }

            return Ok (sections);
        }
    }
}
