using ClassRegistration.DataAccess.Entity;

namespace ClassRegistration.DataAccess.Repository {

    public class Repository {

        protected readonly Course_registration_dbContext _context;

        public Repository (Course_registration_dbContext context) {
            _context = context;
        }
    }
}
