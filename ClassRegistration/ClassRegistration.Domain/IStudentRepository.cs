using ClassRegistration.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ClassRegistration.Domain {
    
    public interface IStudentRepository {

        Task<StudentModel> FindById (int id);

        Task AddEnrollment (int studentId, EnrollmentModel enrollment);
    }
}
