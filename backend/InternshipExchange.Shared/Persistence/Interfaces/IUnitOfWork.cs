namespace InternshipExchange.Shared.Persistence.Interfaces
{
    public interface IUnitOfWork<out TContext> where TContext : Microsoft.EntityFrameworkCore.DbContext
    {
        TContext Context { get; }
        Task SaveAsync();
    }
}
