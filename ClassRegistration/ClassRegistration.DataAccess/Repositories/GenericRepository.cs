using System;
using System.Text;
using System.Collections.Generic;
using ClassRegistration.DataAccess.Entity;
using AutoMapper;
using ClassRegistration.Domain.Model;
using System.Threading.Tasks;
 
namespace ClassRegistration.DataAccess.Repositories
{
    //generic repo of type <DataAccessEntity, DomainModel>
    public class GenericRepository<TDAL, TBLL> 
        where TDAL : DataModel, new()
        where TBLL : BaseBusinessModel, new()
    {

        protected readonly Course_registration_dbContext _context = null;
        protected IMapper mapper;

        public GenericRepository(Course_registration_dbContext _context)
        {
            this._context = _context;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DataAccess.Entity.Course, Domain.Model.Course>();
                cfg.CreateMap<Domain.Model.Course, DataAccess.Entity.Course>();

                cfg.CreateMap<DataAccess.Entity.Section, Domain.Model.Section>();
                cfg.CreateMap<Domain.Model.Section, DataAccess.Entity.Section>();



            });
            mapper = config.CreateMapper();
        }

        
        
    }
}
  