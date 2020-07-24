using ClassRegistration.DataAccess.Interfaces;
using Microsoft.AspNetCore.Mvc;
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
        /// get all the sections available 
        /// <returns></returns>
        // GET: api/section/all
        [HttpGet ("{all}")]
        public async Task<IActionResult> Get ()
        {
            var theClasses = await _sectionRepository.FindAll ();
            return Ok (theClasses);
        }

        /// <summary>
        /// Returns an instructor's sections, courses, and the courses' associated reviews 
        /// </summary>
        /// <param name="instructorId"></param>
        /// <returns></returns>
        // GET api/section?instructorId=50
        [HttpGet]
        public async Task<IActionResult> GetCoursesByInstructorID (int instructorId)
        {
            // get all the sections and associated courses for an instructor
            var theSections = await _sectionRepository.FindByInstrId (instructorId);

            if (!theSections.Any ())
            {
                return NotFound ();
            }
            return Ok (theSections);
        }
    }
}
