using ClassRegistration.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClassRegistration.DataAccess.Interfaces
{
    public interface IReviewsRepository
    {
        Task<bool> AddaReview(int studentid, int courseid, int score, string text);
        Task<IEnumerable<ReviewsModel>> FindAll();

    }
}




