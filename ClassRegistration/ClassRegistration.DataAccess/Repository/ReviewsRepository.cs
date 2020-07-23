using ClassRegistration.DataAccess.Entities;
using ClassRegistration.DataAccess.Interfaces;
using ClassRegistration.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace ClassRegistration.DataAccess.Repository
{
    public class ReviewsRepository : Repository<Reviews, ReviewsModel>
    {
        public ReviewsRepository(Course_registration_dbContext context) : base(context) { }

        public ReviewsRepository() : this(null) { }



        /// <summary>
        /// A student can add a review for a course
        /// </summary>
        /// <param name="studentid"></param>
        /// <param name="courseid"></param>
        /// <param name="score"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public virtual async Task<bool> AddaReview(int studentid, int courseid, int score, string text)
        {
            var reviewDate = DateTime.Today;
            var newReview = new Reviews { CourseId = courseid, StudentId = studentid, Date = reviewDate, Score = score, Text = text };
            _context.Reviews.Add(newReview);
            await _context.SaveChangesAsync();
            return true;

        }


        /// <summary>
        /// Get all the reviews available
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IEnumerable<ReviewsModel>> FindAll()
        {
            var classes = await _context.Reviews.ToListAsync();
            return _mapper.Map<IEnumerable<ReviewsModel>>(classes);
        }
    }
}





