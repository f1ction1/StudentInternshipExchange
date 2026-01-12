using InternshipEx.Modules.Users.Domain.Entities;

namespace InternshipEx.Modules.Users.Application.Interfaces
{
    public interface IProfileRepository
    {
        Task AddAsync(Profile profile, CancellationToken ct = default);
        Task<Profile?> GetByIdAsync(Guid id, CancellationToken ct = default, Func<IQueryable<Profile>, IQueryable<Profile>>? include = null);
        Task Update(Profile profile, CancellationToken ct = default);
    }
}
