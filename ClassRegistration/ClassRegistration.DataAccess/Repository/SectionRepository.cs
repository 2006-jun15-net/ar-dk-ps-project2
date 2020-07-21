using ClassRegistration.DataAccess.Entity;
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
        public SectionRepository (Course_registration_dbContext context) : base (context) { }

        // get access to course navigation properties given an instructor ID
        public virtual async Task<IEnumerable<SectionModel>> FindByInstrId (int instructorId)
        {
            // TODO we want to make stuff like this use the LINQ query syntax
            var sections = await _context.Section.Include (s => s.Course)
                                    .Where (s => s.InstructorId == instructorId).ToListAsync ();

            return _mapper.Map<IEnumerable<SectionModel>> (sections);
        }

        // get all the sections : just to verify in postman
        public virtual async Task<IEnumerable<SectionModel>> FindAll ()
        {
            //    var class = _dbContext.Course.Where(c => c.CourseId == courseId);
            var sections = await _context.Section.ToListAsync ();
            return _mapper.Map<IEnumerable<SectionModel>> (sections);
        }
    }
}
