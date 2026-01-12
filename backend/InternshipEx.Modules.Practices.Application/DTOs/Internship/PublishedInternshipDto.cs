namespace InternshipEx.Modules.Practices.Application.DTOs.Internship
{
    public class PublishedInternshipDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string EmployerName { get; set; } = string.Empty;
        public string? EmployerLogoUrl { get; set; }
        public string CityName { get; set; } = string.Empty;
        public string CountryName { get; set; } = string.Empty;
        public bool IsLiked { get; set; }
        public bool IsRemote { get; set; }
        public string IndustryName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string TextDate { get; set; } = string.Empty;
        public List<string> Skills { get; set; } = new();
    }
}
