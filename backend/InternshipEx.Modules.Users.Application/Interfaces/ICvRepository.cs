using InternshipEx.Modules.Users.Domain.Entities;

namespace InternshipEx.Modules.Users.Application.Interfaces
{
    public interface ICvRepository
    {
        Task<Cv?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<Cv?> GetCvByStudentId(Guid studentId, CancellationToken ct = default);
        Task AddAsync(Cv cv, CancellationToken ct = default);
    }
}
