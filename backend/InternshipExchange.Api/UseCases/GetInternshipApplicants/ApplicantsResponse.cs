using InternshipEx.Modules.Applications.Contracts.Contracts;

namespace InternshipExchange.Api.UseCases.GetInternshipApplicants
{
    public class ApplicantsResponse
    {
        public InternshipInfoDto InternshipInfo { get; set; } = null!;
        public List<ApplicationWithStudentDataDto> Applications { get; set; } = null!;
    }
}
