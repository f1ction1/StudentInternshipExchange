namespace InternshipEx.Modules.Users.Domain.DTOs
{
    public class ResultDto
    {
        public bool IsSuccess { get; }
        public string? Error { get; }
        public string? Message { get; }

        private ResultDto(bool isSuccess, string? error = null, string? message = null)
        {
            IsSuccess = isSuccess;
            Error = error;
            Message = message;
        }

        public static ResultDto Success() => new(true);
        public static ResultDto Failure(string error, string? message = null) => new(false, error, message);
    }
}
