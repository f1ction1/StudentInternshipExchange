namespace InternshipEx.Modules.Auth.Application.DTOs
{
    public class AuthResponseDto
    {
        public bool IsSuccess { get; set; }
        public string? Token { get; set; } = string.Empty;
        public string? Message { get; set; } = string.Empty;
        public Guid? UserId { get; set; } = null;
    }
}
