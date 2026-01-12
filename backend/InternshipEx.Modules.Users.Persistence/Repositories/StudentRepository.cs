using InternshipEx.Modules.Users.Application.Interfaces;
using InternshipEx.Modules.Users.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InternshipEx.Modules.Users.Persistence.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly DbSet<Student> _dbSet;
        private readonly UsersDbContext _context;

        public UsersDbContext Context => _context;

        public StudentRepository(UsersDbContext context)
        {
            _context = context;
            _dbSet = context.Set<Student>();
        }

        public async Task<Student?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await _dbSet.SingleOrDefaultAsync(e => e.ProfileId == id, ct);
        }

        public async Task AddAsync(Student profile, CancellationToken ct = default)
        {
            await _dbSet.AddAsync(profile, ct);
        }

        public Task Update(Student profile, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<Student>> GetByIdsAsync(IReadOnlyCollection<Guid> ids, CancellationToken ct = default)
        {
            return await _dbSet.Include(e => e.Profile).Where(e => ids.Contains(e.ProfileId)).ToListAsync(ct);
        }
    }
}
