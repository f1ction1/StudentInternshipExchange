namespace InternshipEx.Modules.Applications.Contracts.Contracts
{
    public class InternshipInfoDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string CompanyName { get; set; } = null!;
        public string CompanyLocation { get; set; } = null!;
    }
}
