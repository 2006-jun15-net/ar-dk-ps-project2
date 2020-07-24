using ClassRegistration.DataAccess.Interfaces;
using ClassRegistration.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
            var theReviews = await _reviewsRepository.FindAll ();
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
                return BadRequest ();
            }

            await _reviewsRepository.Add (review.StudentId, review.CourseId, review.Score, review.Text);
            return Ok ();
        }
    }
}




