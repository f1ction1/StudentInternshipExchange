using InternshipEx.Modules.Users.Domain.Entities;

namespace InternshipEx.Modules.Users.Application.Interfaces
{
    public interface IStudentRepository
    {
        Task AddAsync(Student profile, CancellationToken ct = default);
        Task<Student?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<IList<Student>> GetByIdsAsync(IReadOnlyCollection<Guid> ids, CancellationToken ct = default);
        Task Update(Student profile, CancellationToken ct = default);
    }
}
