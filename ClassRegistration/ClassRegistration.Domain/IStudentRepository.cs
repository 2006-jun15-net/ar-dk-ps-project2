using ClassRegistration.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ClassRegistration.Domain {
    
    public interface IStudentRepository {

        Task AddEnrollment (int studentId, EnrollmentModel enrollment);

        Task<bool> DeleteEnrollment (int studentId, int enrollmentId);
    }
}
