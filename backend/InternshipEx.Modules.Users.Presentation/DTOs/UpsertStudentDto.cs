using Microsoft.AspNetCore.Http;

namespace InternshipEx.Modules.Users.Endpoints.DTOs
{
    public class UpsertStudentDto
    {
        public string University { get; set; } = string.Empty;
        public string Faculty { get; set; } = string.Empty;
        public string Degree { get; set; } = string.Empty;
        public int YearOfStudy { get; set; }
        public string? Specialization { get; set; }
    }
}
