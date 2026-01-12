using InternshipEx.Modules.Auth.Domain.Entities;
using Modules.Common.Application;

namespace InternshipEx.Modules.Auth.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<int?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<User?> GetByEmailAsync(string email, CancellationToken ct = default);
        Task AddAsync(User user, CancellationToken ct = default);
        Task SaveChangesAsync(CancellationToken ct = default);
    }
}
