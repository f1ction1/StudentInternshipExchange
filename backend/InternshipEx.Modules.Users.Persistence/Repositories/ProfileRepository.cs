using InternshipEx.Modules.Users.Application.Interfaces;
using InternshipEx.Modules.Users.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InternshipEx.Modules.Users.Persistence.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly DbSet<Profile> _dbSet;
        private readonly UsersDbContext _context;

        public UsersDbContext Context => _context;

        public ProfileRepository(UsersDbContext context)
        {
            _context = context;
            _dbSet = context.Set<Profile>();
        }
        //should be refactored, because of include parameter couldn not be used in application layer - it will break abstraction
        public async Task<Profile?> GetByIdAsync(Guid id, CancellationToken ct = default, Func<IQueryable<Profile>, IQueryable<Profile>>? include = null)
        {
            IQueryable<Profile> query = _dbSet;

            if(include != null)
            {
                query = include(query);
            }

            return await query
                .Include(p => p.Employer)
                .Include(p => p.Student)
                .SingleOrDefaultAsync(e => e.Id == id, ct);
        }

        public async Task AddAsync(Profile profile, CancellationToken ct = default)
        {
            await _dbSet.AddAsync(profile, ct);
        }

        public Task Update(Profile profile, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }
    }
}
