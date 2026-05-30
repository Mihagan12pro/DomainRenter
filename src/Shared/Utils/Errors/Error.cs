namespace Utils.Errors
{
    public class Error
    {
        public string Message { get; set; }

        public Error()
        {
            
        }

        public Error(string message)
        {
            Message = message;
        }
    }
}
