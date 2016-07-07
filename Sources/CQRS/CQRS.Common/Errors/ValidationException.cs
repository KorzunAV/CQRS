using System;
using System.Collections.Generic;

namespace CQRS.Common.Errors
{
    public class ValidationException : Exception
    {
        public List<ErrorInfo> ErrorsInfo { get; set; }

        public ValidationException() { }

        public ValidationException(List<ErrorInfo> errors)
        {
            ErrorsInfo = errors;
        }

        public ValidationException(string error, string message)
            : this(error, message, null)
        {
        }

        public ValidationException(string error, string message, Exception exception)
            : base(message, exception)
        {
        }
    }
}