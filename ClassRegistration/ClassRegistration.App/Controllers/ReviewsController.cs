using ClassRegistration.App.ResponseObjects;
using ClassRegistration.DataAccess.Interfaces;
using ClassRegistration.Domain;
using ClassRegistration.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ClassRegistration.App.Controllers
{
    [Route ("api/[controller]")]
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
        // POST: api/reviews
        //[HttpPost]
        //public async Task<IActionResult> Post ([FromBody] ReviewsModel review)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest (new ErrorObject ("Invalid review data sent"));
        //    }

        //    await _reviewsRepository.Add (review.StudentId, review.CourseId, review.Score, review.Text);
        //    return Ok (MessageObject.Success);
        //}





        /// <summary>
        /// Add a review for a course based on a student's name
        /// </summary>
        /// <param name="review"></param>
        /// <returns></returns>
        // POST: api/reviews
        [HttpPost]
        public async Task<IActionResult> Post(string studentname, [FromBody] ReviewsModel review)
        {

            var currentStudent = await _studentRepository.FindByName(studentname);

            if (!ModelState.IsValid)
            {
                return BadRequest(new ErrorObject("Invalid review data sent"));
            }

            await _reviewsRepository.Add(currentStudent, review.CourseId, review.Score, review.Text);
            return Ok(MessageObject.Success);
        }
    }
}




