using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ClassRegistration.DataAccess.Entity;
using ClassRegistration.DataAccess.Repositories;
using ClassRegistration.DataAccess.Interfaces;


namespace ClassRegistration.App.Controllers
{

    [Route("api/[controller]")]
    public class SectionController : ControllerBase
    {

        private readonly ISectionRepository _secRepo;
        

        public SectionController(ISectionRepository secRepo)
        {
            _secRepo = secRepo;
           
        }

         

        // GET api/section/items/50
        [HttpGet("items/{id}")]
        public async Task<ActionResult<Section>> GetCourseByInstructorID(int id)
        {
            //get all the sections and associated courses for an instructor
            var theSections = await _secRepo.GetSectionByInstID(id);
            

            if (!theSections.Any())
            {
                return NotFound();
            }
            return Ok(theSections);


        }



        //get all the sections available - just for self check in postman
        // GET: api/section/items
        [HttpGet("items")]
        public IActionResult GetAllSectionsAvailable()
        {
            //return Ok(_dbContext.Course.ToList());
            var theClasses = _secRepo.GetTheSections();
            return Ok(theClasses);
        }

    }



}
