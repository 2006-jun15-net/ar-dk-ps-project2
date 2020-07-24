using System;

namespace ClassRegistration.App.ResponseObjects
{
    public class ValidationError : ErrorObject
    {
        public ValidationError (ArgumentException e) : base (e.Message) { }
    }
}
