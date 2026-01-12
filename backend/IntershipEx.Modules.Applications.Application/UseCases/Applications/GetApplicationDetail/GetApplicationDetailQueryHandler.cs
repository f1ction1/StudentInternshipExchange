using IntershipEx.Modules.Applications.Application.Abstractions.Data;
using IntershipEx.Modules.Applications.Application.Abstractions.Messaging;
using IntershipEx.Modules.Applications.Domain.Applications;
using Modules.Common.Domain.Abstractions;

namespace IntershipEx.Modules.Applications.Application.UseCases.Applications.GetApplicationDetail;

internal class GetApplicationDetailQueryHandler(
    IUnitOfWork unitOfWork) : IQueryHandler<GetApplicationDetailQuery, ApplicationDetailResponse>
{
    public async Task<Result<ApplicationDetailResponse>> Handle(GetApplicationDetailQuery request, CancellationToken cancellationToken)
    {
        var application = await unitOfWork.Applications.GetByIdAsync(request.Id);
        if (application is null)
        {
            return Result.Failure<ApplicationDetailResponse>(ApplicationErrors.NotFound);
        }

        List<StatusDto> statusHistory = new List<StatusDto>();
        if(application.Status == ApplicationStatus.Applied)
        {
            statusHistory.Add(new StatusDto(application.Status.ToString(), application.CreatedAt.ToLongDateString()));
        } else if(application.Status == ApplicationStatus.Reviewed)
        {
            statusHistory.Add(new StatusDto("Applied", application.CreatedAt.ToLongDateString()));
            statusHistory.Add(new StatusDto(application.Status.ToString(), application.Review?.ReviewedAt.ToLongDateString()!));
        } else if (application.Status == ApplicationStatus.Rejected)
        {
            statusHistory.Add(new StatusDto("Applied", application.CreatedAt.ToLongDateString()));
            statusHistory.Add(new StatusDto(application.Status.ToString(), application.Reject?.RejectedAt.ToLongDateString()!));
        }


        return new ApplicationDetailResponse(
            application.IntershipId,
            application.InternshipInfo.Title,
            application.InternshipInfo.CompanyName,
            application.InternshipInfo.CompanyLocation,
            application.CoverLetter ?? string.Empty,
            application.Reject?.RejectionReason,
            application.Status.ToString(),
            statusHistory
        );
    }
}
