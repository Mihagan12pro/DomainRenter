namespace Utils.Success
{
    public class Success : IResult
    {
        public int StatusCode { get; set; }

        protected dynamic value;

        public object GetValue()
            => value;

        public Success(int statusCode = 200)
        {
            StatusCode = statusCode;
        }
    }

    public class Success<T> : Success
    {
        public T Value
        {
            set
            {
                this.value = value;
            }
            get
            {
                return value;
            }
        }

        public Success(int statusCode = 200)
        {
            StatusCode = statusCode;
        }

        public Success(T successValue, int statusCode = 200)
        {
            StatusCode = statusCode;

            Value = successValue;
        }
    }
}
