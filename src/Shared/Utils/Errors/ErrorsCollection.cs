namespace Utils.Errors
{
    public class ErrorsCollection : List<Error>, IResult
    {
        public int StatusCode { get; }

        public ErrorsCollection(int statusCode = 500, params Error[] errors)
        {
            StatusCode = statusCode;

            foreach (Error error in errors)
                Add(error);
        }

        public ErrorsCollection(int statusCode = 500, params string[] errors)
        {
            StatusCode = statusCode;

            foreach (string error in errors)
                Add(new Error(error));
        }
    }
}
