using System.Linq.Expressions;

namespace InternshipExchange.Shared.Persistence.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(int id, Func<IQueryable<T>, IQueryable<T>>? include = null);
        IQueryable<T> GetAll(Expression<Func<T, bool>>? filter = null);
        Task InsertAsync(T newEntity);
        void Update(T updatedEntity);
    }
}
