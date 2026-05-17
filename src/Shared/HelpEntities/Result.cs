namespace HelpEntities
{
    public class Result
    {
        private bool _isSuccess, _isFail;

        public bool IsSuccess
        {
            get { return _isSuccess; }
            set
            {
                _isSuccess = value;
                _isFail = !value;
            }
        }

        public bool IsFail
        {
            get { return _isFail; }
            set
            {
                _isFail = value;
                _isSuccess = !value;
            }
        }
    }

    public class Result<TValue, TFail> : Result
    {
        private TValue? _value;
        private TFail? _fail;

        public TFail? Fail
        {
            get => _fail;
            set
            {
                _fail = value;

                _value = default;
                IsSuccess = false;
            }
        }

        public TValue? Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;

                _fail = default;
                IsSuccess = true;
            }
        }
    }
}
