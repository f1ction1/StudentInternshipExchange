using IntershipEx.Modules.Applications.Application.Abstractions.Data;
using Microsoft.EntityFrameworkCore;
using ApplicationEntity = IntershipEx.Modules.Applications.Domain.Applications.Application;

namespace IntershipEx.Modules.Applications.Persistence.Repositories;

public class ApplicationRepository : Repository<ApplicationEntity, Guid>, IApplicationRepository
{
    public ApplicationRepository(ApplicationsDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IReadOnlyCollection<ApplicationEntity>> GetApplicationsByStudentAsync(Guid studentId, CancellationToken ct = default)
    {
        return await _dbSet.Where(application => application.StudentId == studentId).ToListAsync();
    }

    public async Task<IList<ApplicationEntity>> GetApplicatiosByInternshipAsync(Guid internshipId, CancellationToken ct = default)
    {
        return await _dbSet.Where(application => application.IntershipId == internshipId).ToListAsync();
    }
}
