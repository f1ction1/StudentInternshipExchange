namespace InternshipEx.Modules.Users.Domain.Entities
{
    public class Employer
    {
        public Guid Id { get; set; }
        public string CompanyName { get; set; } = string.Empty;

        public int CompanySizeId { get; set; }
        public CompanySize CompanySize { get; set; } = null!;

        public string? CompanyDescription { get; set; } = string.Empty;
        public byte[]? CompanyLogo { get; set; } = null;
        public string? CompanyWebsite { get; set; } = string.Empty;

        public virtual ICollection<Profile>? Profiles { get; set; } = new List<Profile>();

        public static Employer Create(
            string companyName,
            int companySizeId,
            string? companyDescription = default,
            string? companyWebsite = default,
            byte[]? companyLogo = default)
        {
            Employer employer = new Employer
            {
                Id = Guid.NewGuid(),
                CompanyName = companyName!,
                CompanySizeId = companySizeId,
                CompanyDescription = companyDescription,
                CompanyLogo = companyLogo,
                CompanyWebsite = companyWebsite
            };
            return employer;
        }
    }
}
