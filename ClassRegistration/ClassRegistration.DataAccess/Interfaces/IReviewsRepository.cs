using ClassRegistration.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClassRegistration.DataAccess.Interfaces
{
    public interface IReviewsRepository
    {
        /// <summary>
        /// A student can add a review for a course
        /// </summary>
        /// <param name="studentid"></param>
        /// <param name="courseid"></param>
        /// <param name="score"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        //Task Add (int studentid, int courseid, int score, string text);


        Task Add(StudentModel student, int courseid, int score, string text);

        /// <summary>
        /// Get all the reviews available
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<ReviewsModel>> FindAll ();
    }
}




