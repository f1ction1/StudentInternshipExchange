namespace IntershipEx.Modules.Applications.Application.Abstractions.Data;
using ApplicationEntity = IntershipEx.Modules.Applications.Domain.Applications.Application;

public interface IApplicationRepository : IRepository<
    ApplicationEntity, Guid>
{
    Task<IReadOnlyCollection<ApplicationEntity>> GetApplicationsByStudentAsync(Guid studentId, CancellationToken ct = default);
    Task<IList<ApplicationEntity>> GetApplicatiosByInternshipAsync(Guid internshipId, CancellationToken ct = default);
}
