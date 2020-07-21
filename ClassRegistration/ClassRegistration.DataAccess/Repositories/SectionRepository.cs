using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassRegistration.DataAccess.Entity;
using ClassRegistration.DataAccess.Interfaces;
using ClassRegistration.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;


namespace ClassRegistration.DataAccess.Repositories
{


    public class SectionRepository : GenericRepository<DataAccess.Entity.Section, Domain.Model.Section>, ISectionRepository
    {



        public SectionRepository(Course_registration_dbContext _context) : base(_context)
        {

        }
        

        


        //get access to course navigation properties given an instructor ID
        public async Task<IEnumerable<Domain.Model.Section>> GetSectionByInstID(int id)
        {
           
            List<Section> thesections = await _context.Section
                .Include(s => s.Course)
                    .Where(s => s.InstructorId == id)
                .ToListAsync();

            return mapper.Map<IEnumerable<Domain.Model.Section>>(thesections);
            
        }


       

        //get all the sections : just to verify in postman
        public IEnumerable<Domain.Model.Section> GetTheSections()
        {
            //    var class = _dbContext.Course.Where(c => c.CourseId == courseId);
            List<Section> sections = _context.Section.ToList();

            var businessSections = mapper.Map<IEnumerable<Domain.Model.Section>>(sections);
            return businessSections;
        }



    }

}
