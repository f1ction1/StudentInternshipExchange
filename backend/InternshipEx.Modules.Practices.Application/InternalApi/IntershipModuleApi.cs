using InternshipEx.Modules.Practices.Application.Interfaces;
using InternshipEx.Modules.Practices.Contracts;
using InternshipEx.Modules.Practices.Contracts.DTOs;
using InternshipEx.Modules.Practices.Domain.Entities.Internships;
using Modules.Common.Domain.Abstractions;

namespace InternshipEx.Modules.Practices.Application.InternalApi
{
    public class IntershipModuleApi(IUnitOfWork unitOfWork) : IInternshipModuleApi
    {
        public async Task<Result<InternshipDTO>> GetInternship(Guid internshipId, CancellationToken cancellationToken)
        {
            var internship = await unitOfWork.Internships.GetByIdAsync(internshipId);
            if(internship == null)
            {
                return Result.Failure<InternshipDTO>(InternshipErrors.NotFound);
            }

            if(internship.InternshipStatus != Domain.Enums.InternshipStatus.Active)
            {
                return Result.Failure<InternshipDTO>(InternshipErrors.NotAcceptingApplications);
            }

            return Result.Success<InternshipDTO>(new InternshipDTO( 
                internship.Title,
                internship.EmployerName,
                internship.City.Name + ", " + internship.Country.Name
            ));
        }
    }

}
