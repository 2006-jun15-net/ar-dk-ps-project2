using AutoMapper;
using ClassRegistration.DataAccess.Entity;
using ClassRegistration.Domain.Model;

namespace ClassRegistration.DataAccess.Repository
{
    public class Repository
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
                cfg.CreateMap<Course, CourseModel> ();
                cfg.CreateMap<CourseModel, Course> ();

                cfg.CreateMap<Enrollment, EnrollmentModel> ();
                cfg.CreateMap<EnrollmentModel, Enrollment> ();

                cfg.CreateMap<Instructor, InstructorModel> ();
                cfg.CreateMap<InstructorModel, Instructor> ();

                cfg.CreateMap<Reviews, ReviewsModel> ();
                cfg.CreateMap<ReviewsModel, Reviews> ();

                cfg.CreateMap<Section, SectionModel> ();
                cfg.CreateMap<SectionModel, Section> ();

                cfg.CreateMap<Student, StudentModel> ();
                cfg.CreateMap<StudentModel, Student> ();
            });

            _mapper = config.CreateMapper ();
        }
    }
}
