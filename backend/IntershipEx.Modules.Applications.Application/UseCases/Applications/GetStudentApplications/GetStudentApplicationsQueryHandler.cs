using Humanizer;
using Modules.Common.Application;
using IntershipEx.Modules.Applications.Application.Abstractions.Data;
using IntershipEx.Modules.Applications.Application.Abstractions.Messaging;
using IntershipEx.Modules.Applications.Domain.Applications;
using Modules.Common.Domain.Abstractions;

namespace IntershipEx.Modules.Applications.Application.UseCases.Applications.GetStudentApplications
{
    internal class GetStudentApplicationsQueryHandler(
        IUnitOfWork unitOfWork,
        ICurrentUserService currentUserService) : IQueryHandler<GetStudentApplicationsQuery, IReadOnlyCollection<ApplicationResponse>>
    {
        public async Task<Result<IReadOnlyCollection<ApplicationResponse>>> Handle(GetStudentApplicationsQuery request, CancellationToken cancellationToken)
        {
            var studentId = currentUserService.UserId;

            var applications = await unitOfWork.Applications.GetApplicationsByStudentAsync(studentId, cancellationToken);

            if (applications == null) 
            {
                return Result.Failure<IReadOnlyCollection<ApplicationResponse>>(ApplicationErrors.NotFound);
            }

            return applications.Select(application => new ApplicationResponse(
                application.Id,
                application.InternshipInfo.Title,
                application.InternshipInfo.CompanyName,
                application.InternshipInfo.CompanyLocation,
                application.Status.ToString(),
                application.CreatedAt.Humanize(dateToCompareAgainst: DateTime.UtcNow))).ToList();
        }
    }

}
