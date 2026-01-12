using Modules.Common.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;


namespace IntershipEx.Modules.Applications.Persistence.Repositories;

public class Repository<TEntity, TId> where TEntity : Entity<TId> where TId : struct
{
    protected readonly ApplicationsDbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public Repository(ApplicationsDbContext context)
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
