using Modules.Common.Domain.Abstractions;

namespace IntershipEx.Modules.Applications.Application.Abstractions.Data
{
    public interface IRepository<TEntity, TId> where TEntity : Entity<TId> where TId : struct
    {
        Task<TEntity?> GetByIdAsync(TId id);
        Task Add(TEntity entity);
    }
}
    