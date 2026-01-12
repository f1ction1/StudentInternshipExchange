using InternshipEx.Modules.Practices.Application.Interfaces;
using InternshipEx.Modules.Practices.Domain.Entities.Internships;
using Modules.Common.Application.Messaging;
using Modules.Common.Domain.Abstractions;

namespace InternshipEx.Modules.Practices.Application.UseCases.Internships.GetInternshipDetails;

internal class GetInternshipDetailsQueryHandler
    (IUnitOfWork unitOfWork): IQueryHandler<GetIntershipDetailsQuery, IntershipDetailResponse>
{
    public async Task<Result<IntershipDetailResponse>> Handle(GetIntershipDetailsQuery request, CancellationToken cancellationToken)
    {
        var internship = await unitOfWork.Internships.GetByIdAsync(request.InternshipId);
        if(internship == null)
        {
            return Result.Failure<IntershipDetailResponse>(InternshipErrors.NotFound);
        }
        if(!internship.IsPublished)
        {
            return Result.Failure<IntershipDetailResponse>(InternshipErrors.Expired);
        }

        var result = new IntershipDetailResponse(
            internship.Title,
            internship.Description,
            internship.City.Name + ", " + internship.Country.Name,
            internship.EmployerId,
            internship.EmployerName,
            internship.IsRemote,
            internship.CreatedAt,
            internship.Skills.Select(skill => skill.Name).ToList());

        return Result.Success(result);
    }
}
