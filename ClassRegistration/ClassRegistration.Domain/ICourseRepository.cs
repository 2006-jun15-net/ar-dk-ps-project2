using ClassRegistration.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClassRegistration.Domain {

    public interface ICourseRepository {

        Task<IEnumerable<CourseModel>> FindByStudent (int studentId);
    }
}
