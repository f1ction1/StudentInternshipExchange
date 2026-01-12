namespace IntershipEx.Modules.Applications.Infrastructure.Controllers.Application
{
    public sealed record ApplyInternshipRequest(
        Guid InternshipId,
        string CoverLetter);

}
