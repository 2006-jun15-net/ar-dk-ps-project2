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
    public class ReviewsRepository : Repository, IReviewsRepository
    {
        public ReviewsRepository (Course_registration_dbContext context) : base (context) { }

        public ReviewsRepository () : this (null) { }

        /// <summary>
        /// A student can add a review for a course
        /// </summary>
        /// <param name="studentid"></param>
        /// <param name="courseid"></param>
        /// <param name="score"></param>
        /// <param name="text"></param>
        /// <returns></returns>

        //add with student's name
        public virtual async Task<bool> Add(StudentModel student, int courseid, int score, string text)
        {
            var reviewDate = DateTime.Today;
            var review = new Reviews
            {
                CourseId = courseid,
                StudentId = student.StudentId,
                Date = reviewDate,
                Score = score,
                Text = text
            };

            await _context.Reviews.AddAsync (review);
            return await _context.SaveChangesAsync () > 0;
        }


        /// <summary>
        /// Get all the reviews available
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IEnumerable<ReviewsModel>> FindAll ()
        {
            var classes = await _context.Reviews.ToListAsync ();
            return _mapper.Map<IEnumerable<ReviewsModel>> (classes);
        }
    }
}
