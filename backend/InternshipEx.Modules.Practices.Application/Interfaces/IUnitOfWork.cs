using InternshipEx.Modules.Practices.Domain.Entities;

namespace InternshipEx.Modules.Practices.Application.Interfaces
{
    public interface IUnitOfWork
    {
        IDictionaryRepository<Skill> Skills { get; }
        IDictionaryRepository<City> Cities { get; }
        IDictionaryRepository<Industry> Industries { get; }
        IDictionaryRepository<Country> Countries { get; }
        IInternshipRepository Internships { get; }
        IInternshipFavoriteRepository InternshipFavorites { get; }
        public Task SaveChangesAsync(CancellationToken ct = default);
    }
}
