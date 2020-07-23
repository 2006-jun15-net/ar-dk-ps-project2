//using ClassRegistration.DataAccess.Entities;
using ClassRegistration.DataAccess.Interfaces;
using ClassRegistration.DataAccess.Repository;
using ClassRegistration.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;



namespace ClassRegistration.App.Controllers
{
    [Route("api/[controller]")]
    public class ReviewsController : ControllerBase
    {
        private readonly ReviewsRepository _reviewsRepository;
        

        public ReviewsController(ReviewsRepository reviewsRepository)
        {
           
            _reviewsRepository = reviewsRepository;
        }


        //to test in postman for adding a review: this returns all the reviews
        // GET: api/reviews
        [HttpGet]
        public async Task<IActionResult> GetItems()
        {
            var theReviews = await _reviewsRepository.FindAll();
            return Ok(theReviews);
        }
        


        //add a review for a course
        // POST: api/reviews/item
        [HttpPost("{item}")]
        public async Task<ActionResult<ReviewsModel>> AddCourseReview([FromBody] ReviewsModel review)
        {
            
            await _reviewsRepository.AddaReview(review.StudentId, review.CourseId, review.Score, review.Text);

            
            return Ok();

        }

    }
}




