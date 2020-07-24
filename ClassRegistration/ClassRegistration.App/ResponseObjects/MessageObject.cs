namespace ClassRegistration.App.ResponseObjects
{
    public class MessageObject
    {
        public static readonly MessageObject Success = new MessageObject ("Success");

        public string Message { get; set; }

        public MessageObject (string message)
        {
            Message = message;
        }
    }
}
