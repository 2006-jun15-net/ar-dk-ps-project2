using AutoMapper;
using ClassRegistration.DataAccess.Entity;
using ClassRegistration.Domain.Model;

namespace ClassRegistration.DataAccess.Repository
{
    public class Repository<T, U> where T : DataModel, new () where U : BaseBusinessModel, new ()
    {
        /// <summary>
        /// Database context instance
        /// </summary>
        protected readonly Course_registration_dbContext _context;
        protected readonly IMapper _mapper;

        public Repository (Course_registration_dbContext context)
        {
            _context = context;

            var config = new MapperConfiguration (cfg =>
            {
                cfg.CreateMap<T, U> ();
                cfg.CreateMap<U, T> ();
                /*
                cfg.CreateMap<Course, CourseModel> ();
                cfg.CreateMap<CourseModel, Course> ();

                cfg.CreateMap<Section, SectionModel> ();
                cfg.CreateMap<SectionModel, Section> ();

                cfg.CreateMap<Enrollment, EnrollmentModel> ();
                cfg.CreateMap<EnrollmentModel, Enrollment> ();

                cfg.CreateMap<Student, StudentModel> ();
                cfg.CreateMap<StudentModel, Student> ();*/
            });
            _mapper = config.CreateMapper();
        }
    }
}
