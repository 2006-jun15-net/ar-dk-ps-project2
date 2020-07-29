using ClassRegistration.App.ResponseObjects;
using ClassRegistration.DataAccess.Interfaces;
using ClassRegistration.Domain;
using ClassRegistration.Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ClassRegistration.App.Controllers
{
    [Route ("api/[controller]")]
    [Authorize]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewsRepository _reviewsRepository;
        private readonly IStudentRepository _studentRepository;

        public ReviewsController (IReviewsRepository reviewsRepository, IStudentRepository studentRepository)
        {
            _reviewsRepository = reviewsRepository;
            _studentRepository = studentRepository;
        }

        /// <summary>
        /// Returns all available reviews
        /// </summary>
        /// <returns></returns>
        // GET: api/reviews/5
        [HttpGet ("{courseId}")]
        public async Task<IActionResult> Get (int courseId)
        {
            var theReviews = await _reviewsRepository.FindByCourse (courseId);
            return Ok (theReviews);
        }

        /// <summary>
        /// Add a review for a course based on a student's name
        /// </summary>
        /// <param name="review"></param>
        /// <returns></returns>
        // POST: api/reviews
        [HttpPost]
        public async Task<IActionResult> Post ([FromBody] ReviewsModel review)
        {
            var currentStudent = await _studentRepository.FindById (review.StudentId);

            if (!ModelState.IsValid)
            {
                return BadRequest (new ErrorObject ("Invalid review data sent"));
            }

            var success = await _reviewsRepository.Add (currentStudent, review.CourseId, review.Score, review.Text);

            if (!success)
            {
                return BadRequest ("Failed to add review");
            }

            return Ok (MessageObject.Success);
        }
    }
}




