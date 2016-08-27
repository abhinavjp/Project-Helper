using System;

namespace HelperProject.Filters
{
    public class ErrorHandler
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public ErrorHandler(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }

        public ErrorHandler(bool isSuccess) : this(isSuccess, "")
        {
        }

        public ErrorHandler(string message) : this(false, message)
        {
        }

        public ErrorHandler(Exception exception): this(false, exception.Message)
        {

        }
    }

    public class ErrorHandler<T>: ErrorHandler
    {
        public T Result { get; set; }
        public ErrorHandler(bool isSuccess, string message): base(isSuccess, message)
        {
        }

        public ErrorHandler(bool isSuccess) : this(isSuccess, "")
        {
        }

        public ErrorHandler(string message) : this(false, message)
        {
        }

        public ErrorHandler(Exception exception) : this(false, exception.Message)
        {

        }

        public ErrorHandler(T TResult, string message): this(true, message)
        {
            Result = TResult;
        }
    }
}