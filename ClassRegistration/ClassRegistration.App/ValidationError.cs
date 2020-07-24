using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassRegistration.App
{
    public class ValidationError
    {
        public string Message { get; set; }

        public ValidationError (ArgumentException e)
        {
            Message = e.Message;
        }
    }
}
