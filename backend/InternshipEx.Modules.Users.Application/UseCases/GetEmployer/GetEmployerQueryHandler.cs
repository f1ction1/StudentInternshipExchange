using InternshipEx.Modules.Users.Application.Interfaces;
using InternshipEx.Modules.Users.Domain.DTOs;
using Modules.Common.Application;
using Modules.Common.Application.Exceptions;
using MediatR;

namespace InternshipEx.Modules.Users.Application.UseCases.GetEmployer
{
    public class GetEmployerQueryHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService) : IRequestHandler<GetEmployerQuery, EmployerDto>
    {
        public async Task<EmployerDto> Handle(GetEmployerQuery request, CancellationToken cancellationToken)
        {
            var id = currentUserService.UserId;

            var profile = await unitOfWork.Profiles.GetByIdAsync(id, cancellationToken);
            
            if (profile is null)
                throw new NotFoundException("Profile not found");

            if (profile.Employer is null)
                throw new BadRequestException("Profile has no associated employer");

            var employer = profile.Employer;

            return new EmployerDto
            {
                CompanyName = employer.CompanyName,
                CompanySizeId = employer.CompanySizeId,
                CompanyDescription = employer.CompanyDescription,
                CompanyWebsite = employer.CompanyWebsite,
            };
        }
    }
}
