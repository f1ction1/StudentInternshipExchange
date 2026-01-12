using InternshipEx.Modules.Practices.Application.DTOs.Internship;
using InternshipEx.Modules.Practices.Application.UseCases.Internships.GetIntershipsList;
using InternshipEx.Modules.Practices.Application.UseCases.Internships.GetPublishedIntershipsList;
using InternshipEx.Modules.Practices.Domain.Entities;

namespace InternshipEx.Modules.Practices.Application.Interfaces
{
    public interface IInternshipRepository : IRepository<Internship, Guid>
    {
        Task<IList<Internship>> GetInternshipsByEmployer(Guid employerId);
        Task<IList<Internship>> GetFavoriteInternshipsAsync(Guid studentId);
        Task<IReadOnlyCollection<Internship>> GetAll();
        Task<IPagedList<PublishedInternshipDto>> GetPublishedInternshipsAsync(GetPublishedInternshipsListQuery query, Guid studentId);
    }
}
