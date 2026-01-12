using InternshipEx.Modules.Practices.Application.Interfaces;
using Modules.Common.Application;
using Modules.Common.Application.Messaging;
using Modules.Common.Domain.Abstractions;

namespace InternshipEx.Modules.Practices.Application.UseCases.Internships.GetLikedInternships
{
    internal class GetLikedInternshipsQueryHandler(
        IUnitOfWork unitOfWork,
        ICurrentUserService currentUserService) : IQueryHandler<GetLikedInternshipsQuery, IList<LikedInternship>>
    {
        public async Task<Result<IList<LikedInternship>>> Handle(GetLikedInternshipsQuery request, CancellationToken cancellationToken)
        {
            var studentId = currentUserService.UserId;
            var internships = await unitOfWork.Internships.GetFavoriteInternshipsAsync(studentId);
            if(internships == null)
            {
                return Result.Failure<IList<LikedInternship>>(new Error("Internships.NotFound", "No liked internships found."));
            }
            return internships.Select(i => new LikedInternship(
                    i.Id,
                    i.Title,
                    i.EmployerName,
                    i.City.Name + ", " + i.Country.Name,
                    i.IsRemote))
                .ToList();
        }
    }
}
