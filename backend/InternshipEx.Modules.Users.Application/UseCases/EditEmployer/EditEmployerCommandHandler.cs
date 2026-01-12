using InternshipEx.Modules.Users.Application.Interfaces;
using Modules.Common.Application;
using Modules.Common.Application.Exceptions;
using MediatR;

namespace InternshipEx.Modules.Users.Application.UseCases.EditEmployer
{
    public class EditEmployerCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService) : IRequestHandler<EditEmployerCommand>
    {
        public async Task Handle(EditEmployerCommand request, CancellationToken cancellationToken)
        {
            Guid id = currentUserService.UserId;

            var profile = await unitOfWork.Profiles.GetByIdAsync(id,cancellationToken);
            
            if (profile is null) 
                throw new NotFoundException("Profile not found");
            var employer = profile.Employer;
            if (employer is null)
                throw new BadRequestException("Profile has no associated employer");

            employer.CompanyName = request.Employer.CompanyName;
            employer.CompanySizeId = request.Employer.CompanySizeId;
            employer.CompanyDescription = request.Employer.CompanyDescription;
            employer.CompanyWebsite = request.Employer.CompanyWebsite;

            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
