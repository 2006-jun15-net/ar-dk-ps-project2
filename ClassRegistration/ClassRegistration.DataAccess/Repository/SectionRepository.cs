using ClassRegistration.DataAccess.Entity;
using ClassRegistration.DataAccess.Interfaces;
using ClassRegistration.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassRegistration.DataAccess.Repository
{
    public class SectionRepository : Repository, ISectionRepository
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
            List<Section> sections = await _context.Section.ToListAsync();
            return _mapper.Map<IEnumerable<SectionModel>>(sections);
        }

        public virtual async Task<SectionModel> FindById (int id)
        {
            var section = await (from s in _context.Section
                                 where s.SectId == id
                                 select s).FirstOrDefaultAsync ();

            return _mapper.Map<SectionModel> (section);
        }

        /// <summary>
        /// Find the courses and their reveiws by instructor ID
        /// </summary>
        /// <param name="instructorId"></param>
        /// <returns></returns>
        // get access to course navigation properties given an instructor ID
        public virtual async Task<IEnumerable<SectionModel>> FindByInstrId (int instructorId)
        {
            var sections = await _context.Section.Include (s => s.Course).ThenInclude (c => c.Reviews)
                                    .Where (s => s.InstructorId == instructorId).ToListAsync ();

            return _mapper.Map<IEnumerable<SectionModel>> (sections);
        }

        public virtual async Task<bool> Add (int instructorId, int courseId, string term, TimeSpan start, TimeSpan end)
        {
            var section = new Section
            {
                InstructorId = instructorId,
                CourseId = courseId,
                Term = term,
                StartTime = start,
                EndTime = end
            };

            await _context.Section.AddAsync (section);
            return await _context.SaveChangesAsync () > 0;
        }
    }
}

       
    


        




