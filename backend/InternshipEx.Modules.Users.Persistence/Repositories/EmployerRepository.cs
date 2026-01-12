using InternshipEx.Modules.Users.Application.Interfaces;
using InternshipEx.Modules.Users.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InternshipEx.Modules.Users.Persistence.Repositories
{
    public class EmployerRepository : IEmployerRepository
    {
        private readonly DbSet<Employer> _dbSet;
        private readonly UsersDbContext _context;

        public UsersDbContext Context => _context;

        public EmployerRepository(UsersDbContext context)
        {
            _context = context;
            _dbSet = context.Set<Employer>();
        }

        public async Task<Employer?> GetByIdAsync(Guid id, CancellationToken ct = default, Func<IQueryable<Employer>, IQueryable<Employer>>? include = null)
        {
            IQueryable<Employer> query = _dbSet;

            if (include != null)
            {
                query = include(query);
            }

            return await query.SingleOrDefaultAsync(e => e.Id == id, ct);
        }

        public async Task AddAsync(Employer employer, CancellationToken ct = default)
        {
            await _dbSet.AddAsync(employer, ct);
        }

        public async Task<Employer?> GetByProfileIdAsync(Guid profileId)
        {
            return await _dbSet.Include(e => e.Profiles).FirstOrDefaultAsync(e => e.Profiles != null && e.Profiles.Any(p => p.Id == profileId));
        }
    }
}
