using System;
using System.Collections.Generic;
using System.Text;
using ClassRegistration.Domain.Model;
using ClassRegistration.DataAccess.Entity;

namespace ClassRegistration.DataAccess.Repositories
{
    interface IGenericRepository<TDAL, TBLL>
        where TDAL : DataModel, new()
        where TBLL : BaseBusinessModel, new()
    {
    }
}


    

