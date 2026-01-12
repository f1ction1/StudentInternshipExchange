using InternshipEx.Modules.Applications.Contracts.Contracts;

namespace InternshipExchange.Api.UseCases.GetInternshipApplicants
{
    public class ApplicationWithStudentDataDto : ApplicationDto
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string University { get; set; } = null!;
        public string? Specialization { get; set; }
        public int YearOfStudy { get; set; }
    }
}
