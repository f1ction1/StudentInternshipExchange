using IntershipEx.Modules.Applications.Application.Abstractions.Data;
using IntershipEx.Modules.Applications.Persistence.Repositories;

namespace IntershipEx.Modules.Applications.Persistence.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationsDbContext _context;
    private IApplicationRepository? _applicationRepository;

    public UnitOfWork(ApplicationsDbContext context)
    {
        _context = context;
    }

    public ApplicationsDbContext Context => _context;
    public IApplicationRepository Applications => _applicationRepository ??= new ApplicationRepository(Context);

    public async Task SaveChangesAsync(CancellationToken ct = default)
    {
        await _context.SaveChangesAsync(ct);
    }
}
