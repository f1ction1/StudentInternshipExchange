using InternshipEx.Modules.Practices.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InternshipEx.Modules.Practices.Persistence.Repositories
{
    public class DictionaryRepository<T> : IDictionaryRepository<T> where T : class
    {
        private readonly DbSet<T> _dbSet;
        private readonly PracticesDbContext _context;

        public DictionaryRepository(PracticesDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public IList<T> GetWithCondition(Func<T, bool> predicate)
        {
            return _dbSet.Where(predicate).ToList();
        }
    }
}
