namespace InternshipEx.Modules.Users.Domain.DTOs
{
    public class EmployerDto
    {
        public string CompanyName { get; set; } = string.Empty;
        public int CompanySizeId { get; set; }
        public string? CompanyDescription { get; set; } = string.Empty;
        //public byte[]? CompanyLogo { get; set; } = null;
        public string? CompanyWebsite { get; set; } = string.Empty;
    }
}
