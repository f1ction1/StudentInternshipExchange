using InternshipEx.Modules.Users.Application.Interfaces;
using InternshipEx.Modules.Users.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InternshipEx.Modules.Users.Persistence.Repositories
{
    public class CvRepository : ICvRepository
    {
        private readonly DbSet<Cv> _dbSet;
        private readonly UsersDbContext _context;

        public UsersDbContext Context => _context;

        public CvRepository(UsersDbContext context)
        {
            _context = context;
            _dbSet = context.Set<Cv>();
        }

        public async Task<Cv?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await _dbSet.SingleOrDefaultAsync(e => e.Id == id, ct);
        }

        public async Task AddAsync(Cv cv, CancellationToken ct = default)
        {
            await _dbSet.AddAsync(cv, ct);
        }

        public async Task<Cv?> GetCvByStudentId(Guid studentId, CancellationToken ct = default)
        {
            return await _dbSet.SingleOrDefaultAsync(e => e.StudentId == studentId, ct);
        }
    }
}
