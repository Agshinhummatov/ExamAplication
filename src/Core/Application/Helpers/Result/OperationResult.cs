namespace Application.Helpers.Result
{
    public class OperationResult
    {
        public bool Success { get; }
        public bool Fail => !Success;
        public string? Message { get; set; }
        public string[]? Errors { get; set; }
        public int StatusCode { get; set; }

        public OperationResult(bool status, string? message)
        {
            Success = status;
            Message = message;
            StatusCode = status ? 200 : 400;
        }

        public OperationResult() { }

        public OperationResult(bool status, string? message, string[]? errors)
        {
            Success = status;
            Message = message;
            Errors = errors;
            StatusCode = status ? 200 : 400;
        }

        public static OperationResult Failed(string? message = null, string[]? errors = null)
        {
            return new OperationResult(false, message, errors) { StatusCode = 400 };
        }

        public static OperationResult Succeed(string? message = null)
        {
            return new OperationResult(true, message) { StatusCode = 200 };
        }
    }
}