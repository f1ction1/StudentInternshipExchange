namespace InternshipEx.Modules.Users.Domain.DTOs
{
    public class StudentDto
    {
        public string University { get; set; } = string.Empty;
        public string Faculty { get; set; } = string.Empty;
        public string Degree { get; set; } = string.Empty;
        public int YearOfStudy { get; set; }
        public string? Specialization { get; set; }
    }
}
