using InternshipEx.Modules.Practices.Domain.Entities;
using System.Linq.Expressions;

namespace InternshipEx.Modules.Practices.Application.Interfaces
{
    public interface IRepository<TEntity, TId> where TEntity : Entity<TId> where TId : struct
    {
        Task<TEntity?> GetByIdAsync(TId id);
        Task Add(TEntity entity);
        void Update(TEntity entity);
    }
}
    