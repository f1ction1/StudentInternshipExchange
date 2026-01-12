using InternshipEx.Modules.Practices.Application.DTOs.Internship;
using InternshipEx.Modules.Practices.Application.Interfaces;
using InternshipEx.Modules.Practices.Application.UseCases.Internships.GetIntershipsList;
using Modules.Common.Application;
using MediatR;

namespace InternshipEx.Modules.Practices.Application.UseCases.Internships.GetPublishedIntershipsList
{
    public class GetPublishedInternshipsListQueryHandler(
        IUnitOfWork unitOfWork,
        ICurrentUserService currentUserService) : IRequestHandler<GetPublishedInternshipsListQuery, IPagedList<PublishedInternshipDto>>
    {
        public async Task<IPagedList<PublishedInternshipDto>> Handle(GetPublishedInternshipsListQuery request, CancellationToken cancellationToken)
        {
            var studentId = currentUserService.UserId;
            var pagedList = await unitOfWork.Internships.GetPublishedInternshipsAsync(request, studentId);

            return pagedList;
        }
    }
}
