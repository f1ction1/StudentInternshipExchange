using IntershipEx.Modules.Applications.Application.Abstractions.Messaging;

namespace IntershipEx.Modules.Applications.Application.UseCases.Applications.GetStudentApplications
{
    public record GetStudentApplicationsQuery : IQuery<IReadOnlyCollection<ApplicationResponse>>;
}
