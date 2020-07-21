using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassRegistration.DataAccess.Interfaces
{
    /// <summary>
    /// This interface includes common functionality that is needed by the Enrollment repository
    /// </summary>
    public interface IEnrollmentRepository
    {

        int GetCredits(int id, string term);

        void Save();



    }
}
