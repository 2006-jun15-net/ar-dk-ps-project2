using ClassRegistration.DataAccess.Interfaces;
using ClassRegistration.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

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
        // GET: api/section or api/section?instructorId=5
        [HttpGet]
        public async Task<IActionResult> Get ([FromQuery] int? instructorId = null)
        {
            IEnumerable<SectionModel> sections;

            if (instructorId != null) 
            {
                sections = await _sectionRepository.FindByInstrId (Convert.ToInt32(instructorId));
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




        //using instructor's last name here
        // GET: api/section/{Erickson}
        [HttpGet("{instructorname}")]
        public async Task<IActionResult> GetCoursesandReviews (string instructorname)
        {
            IEnumerable<SectionModel> sections;

            if (instructorname != null)
            {
                sections = await _sectionRepository.FindByInstrName(instructorname);
            }
            else
            {
                sections = await _sectionRepository.FindAll();
            }

            if (!sections.Any())
            {
                return NoContent();
            }

            return Ok(sections);
        }
    }
}
