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
    }
}
