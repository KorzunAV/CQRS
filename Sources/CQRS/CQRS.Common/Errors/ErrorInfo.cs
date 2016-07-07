namespace CQRS.Common.Errors
{
    public class ErrorInfo
    {
        public ErrorInfo() : this(string.Empty) { }

        public ErrorInfo(string errorMessage)
            : this(string.Empty, errorMessage)
        { }

        public ErrorInfo(string key, string errorMessage)
        {
            Key = key;
            ErrorMessage = errorMessage;
        }

        public string Key { get; set; }

        public string ErrorMessage { get; set; }

        public override string ToString()
        {
            return $"Key: {Key}, ErrorMessage: {ErrorMessage}";
        }
    }
}