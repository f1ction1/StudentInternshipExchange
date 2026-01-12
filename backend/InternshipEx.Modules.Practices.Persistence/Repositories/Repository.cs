using InternshipEx.Modules.Practices.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace InternshipEx.Modules.Practices.Persistence.Repositories
{
    public class Repository<TEntity, TId> where TEntity : Entity<TId> where TId : struct
    {
        protected readonly PracticesDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public Repository(PracticesDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }
        public virtual async Task<TEntity?> GetByIdAsync(TId id)
        {
            return await _dbSet.SingleOrDefaultAsync(e => e.Id.Equals(id));
        }

        public async Task Add(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(TEntity entity)
        { 
            _dbSet.Update(entity);
        }
    }
}
