namespace InternshipEx.Modules.Practices.Application.UseCases.Internships.GetLikedInternships
{
    public record LikedInternship(
        Guid InternshipId,
        string Title,
        string EmployerName,
        string Location,
        bool IsRemote);
}
