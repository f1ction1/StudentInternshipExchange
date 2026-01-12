using Modules.Common.Application;

namespace IntershipEx.Modules.Applications.Application.Abstractions.Data
{
    public interface IUnitOfWork
    {
        public IApplicationRepository Applications { get; }
        Task SaveChangesAsync(CancellationToken ct = default);
    }

}
