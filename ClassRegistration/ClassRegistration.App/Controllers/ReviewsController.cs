using ClassRegistration.App.ResponseObjects;
using ClassRegistration.DataAccess.Interfaces;
using ClassRegistration.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClassRegistration.App.Controllers
{
    [Route ("api/[controller]")]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewsRepository _reviewsRepository;

        public ReviewsController (IReviewsRepository reviewsRepository)
        {
            _reviewsRepository = reviewsRepository;
        }

        /// <summary>
        /// Returns all available reviews
        /// </summary>
        /// <returns></returns>
        // GET: api/reviews
        [HttpGet]
        public async Task<IActionResult> Get ()
        {
            IEnumerable<ReviewsModel> theReviews;

            try
            {
                theReviews = await _reviewsRepository.FindAll ();
            }
            catch (ArgumentException e)
            {
                return BadRequest (new ValidationError (e));
            }
            return Ok (theReviews);
        }

        /// <summary>
        /// Add a review for a course
        /// </summary>
        /// <param name="review"></param>
        /// <returns></returns>
        // POST: api/reviews/item
        [HttpPost ("{item}")]
        public async Task<IActionResult> Add ([FromBody] ReviewsModel review)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest (new ErrorObject ("Invalid review data sent"));
            }

            await _reviewsRepository.Add (review.StudentId, review.CourseId, review.Score, review.Text);
            return Ok (MessageObject.Success);
        }
    }
}




