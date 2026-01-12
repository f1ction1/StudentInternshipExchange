using InternshipEx.Modules.Users.Domain.Entities;

namespace InternshipEx.Modules.Users.Application.Interfaces
{
    public interface IEmployerRepository
    {
        Task AddAsync(Employer profile, CancellationToken ct = default);
        Task<Employer?> GetByIdAsync(Guid id, CancellationToken ct = default, Func<IQueryable<Employer>, IQueryable<Employer>>? include = null);
        Task<Employer?> GetByProfileIdAsync(Guid profileId);

    }
}
