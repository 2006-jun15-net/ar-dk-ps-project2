namespace ClassRegistration.DataAccess.Repository
{
    public class Repository
    {
        /// <summary>
        /// A protected field for the database entity
        /// </summary>
        protected readonly Course_registration_dbContext _context;

        public Repository (Course_registration_dbContext context)
        {
            _context = context;
        }
    }
}
