namespace ClassRegistration.App.ResponseObjects
{
    public class ErrorObject : MessageObject
    {
        public string ErrorMessage { get; set; }

        public ErrorObject (string message) : base ("")
        {
            ErrorMessage = message;
        }
    }
}
