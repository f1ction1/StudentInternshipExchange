namespace InternshipEx.Modules.Applications.Contracts.Contracts
{
    public class ApplicationsByInternshipResponse
    {
        public InternshipInfoDto InternshipInfo { get; set; } = null!;
        public List<ApplicationDto> Applications { get; set; } = null!;
    }
}
