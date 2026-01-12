using InternshipEx.Modules.Practices.Application.DTOs.Internship;
using InternshipEx.Modules.Practices.Application.UseCases.Internships.GetPublishedIntershipsList;
using MediatR;

namespace InternshipEx.Modules.Practices.Application.UseCases.Internships.GetIntershipsList
{
    public class GetPublishedInternshipsListQuery : IRequest<IPagedList<PublishedInternshipDto>>
    {
        // Search filters
        public string? SearchTerm { get; set; }
        public int? CityId { get; set; }
        public int? CountryId { get; set; }
        public List<int>? IndustryIds { get; set; }
        public List<int>? SkillIds { get; set; }
        public bool? IsRemote { get; set; }
        // Pagination
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        // Sort always by create date
    }
}
