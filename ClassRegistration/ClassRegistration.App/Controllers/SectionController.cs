using ClassRegistration.DataAccess.Entity;
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

        // GET api/section?instructorId=50
        [HttpGet]
        public async Task<ActionResult<Section>> GetCourseByInstructorID (int instructorId)
        {
            // get all the sections and associated courses for an instructor
            var theSections = await _sectionRepository.FindByInstrId (instructorId);

            if (!theSections.Any ())
            {
                return NotFound ();
            }
            return Ok (theSections);
        }

        // get all the sections available - just for self check in postman
        // GET: api/section
        //[HttpGet]
        //public IActionResult GetAllSectionsAvailable()
        //{
        //    //return Ok(_dbContext.Course.ToList());
        //    var theClasses = _secRepo.GetTheSections();
        //    return Ok(theClasses);
        //}
    }
}
