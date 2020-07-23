using ClassRegistration.DataAccess.Interfaces;
using ClassRegistration.Domain.Model;
using Microsoft.AspNetCore.Mvc;
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

        // to test in postman for adding a review: this returns all the reviews
        // GET: api/reviews
        [HttpGet]
        public async Task<IActionResult> GetItems ()
        {
            var theReviews = await _reviewsRepository.FindAll ();
            return Ok (theReviews);
        }

        // add a review for a course
        // POST: api/reviews/item
        [HttpPost ("{item}")]
        public async Task<ActionResult<ReviewsModel>> AddCourseReview ([FromBody] ReviewsModel review)
        {
            await _reviewsRepository.AddReview (review.StudentId, review.CourseId, review.Score, review.Text);

            return Ok ();
        }
    }
}




