using ClassRegistration.DataAccess.Entities;
using ClassRegistration.DataAccess.Interfaces;
using ClassRegistration.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassRegistration.DataAccess.Repository
{
    public class SectionRepository : Repository<Section, SectionModel>, ISectionRepository
    {
        public SectionRepository(Course_registration_dbContext context) : base(context) { }

        public SectionRepository() : this(null) { }


        /// <summary>
        /// All the sections available
        /// </summary>
        /// <returns></returns>
        // get all the sections : just to verify in postman
        public virtual async Task<IEnumerable<SectionModel>> FindAll()
        {

            var sections = await _context.Section.ToListAsync();

            return _mapper.Map<IEnumerable<SectionModel>>(sections);
        }



        /// <summary>
        /// Find the courses and their reveiws by instructor ID
        /// </summary>
        /// <param name="instructorId"></param>
        /// <returns></returns>
        // get access to course navigation properties given an instructor ID
        public virtual async Task<IEnumerable<Section>> FindByInstrId (int instructorId)
        {
            // we want to make stuff like this use the LINQ query syntax
            var sections = await _context.Section
               .Include(s => s.Course)
                .ThenInclude(c => c.Reviews)
               .Where(s => s.InstructorId == instructorId).ToListAsync();

            return sections;
            

        }

    }
}

       
    


        




