using InternshipEx.Modules.Practices.Domain.Entities;
using InternshipEx.Modules.Practices.Domain.Entities.InternshipFavorites;

namespace InternshipEx.Modules.Practices.Application.Interfaces
{
    public interface IInternshipFavoriteRepository
    {
        Task<InternshipFavorite?> GetFavoriteAsync(Guid internshipId, Guid studentId);
        Task AddFavoriteAsync(InternshipFavorite favorite);
        Task RemoveFavoriteAsync(InternshipFavorite favorite);
        //Task<IReadOnlyCollection<InternshipFavorite>> GetFavoritesByStudentAsync(Guid studentId);
    }
}
