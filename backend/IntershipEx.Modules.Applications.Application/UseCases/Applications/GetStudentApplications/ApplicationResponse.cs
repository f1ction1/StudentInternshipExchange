namespace IntershipEx.Modules.Applications.Application.UseCases.Applications.GetStudentApplications
{
    public record ApplicationResponse(
        Guid Id,
        string InternshipTitle,
        string CompanyName,
        string CompanyLocation,
        string Status,
        string RelativeTime);
}
