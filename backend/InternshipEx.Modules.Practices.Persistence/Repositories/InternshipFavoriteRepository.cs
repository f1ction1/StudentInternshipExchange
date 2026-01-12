using InternshipEx.Modules.Practices.Application.Interfaces;
using InternshipEx.Modules.Practices.Domain.Entities.InternshipFavorites;
using Microsoft.EntityFrameworkCore;

namespace InternshipEx.Modules.Practices.Persistence.Repositories
{
    internal class InternshipFavoriteRepository : IInternshipFavoriteRepository
    {
        protected readonly PracticesDbContext _context;
        protected readonly DbSet<InternshipFavorite> _dbSet;

        public InternshipFavoriteRepository(PracticesDbContext context)
        {
            _context = context;
            _dbSet = context.Set<InternshipFavorite>();
        }
        public async Task AddFavoriteAsync(InternshipFavorite favorite)
        {
            await _dbSet.AddAsync(favorite);
        }

        public async Task<InternshipFavorite?> GetFavoriteAsync(Guid internshipId, Guid studentId)
        {
            return await _dbSet.FirstOrDefaultAsync(f => f.InternshipId == internshipId && f.StudentId == studentId);
        }

        public async Task RemoveFavoriteAsync(InternshipFavorite favorite)
        {
            _dbSet.Remove(favorite);
        }
    }
}
