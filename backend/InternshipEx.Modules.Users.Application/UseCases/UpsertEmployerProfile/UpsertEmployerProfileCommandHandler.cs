using InternshipEx.Modules.Users.Application.Interfaces;
using InternshipEx.Modules.Users.Application.UseCases.CompleteEmployerProfile;
using InternshipEx.Modules.Users.Domain.Entities;
using Modules.Common.Application;
using MediatR;

namespace InternshipEx.Modules.Users.Application.UseCases.UpsertEmployerProfile
{
    public class UpsertEmployerProfileCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService) : IRequestHandler<UpsertEmployerProfileCommand>
    {
        public async Task Handle(UpsertEmployerProfileCommand request, CancellationToken cancellationToken)
        {
            Guid userId = currentUserService.UserId;

            var profile = await unitOfWork.Profiles.GetByIdAsync(userId, cancellationToken);
            if (profile is not null)
            {
                profile.FirstName = request.FirstName;
                profile.LastName = request.LastName;
                profile.PhoneNumber = request.PhoneNumber;
            }
            else
            {
                profile = Profile.Create(userId, request.FirstName, request.LastName, request.PhoneNumber);
                await unitOfWork.Profiles.AddAsync(profile, cancellationToken);
            }
            
            if(profile.Employer is null)
            {
                profile.Employer = Employer.Create(
                    request.CompanyName, request.CompanySizeId, request.CompanyDescription,request.CompanyWebsite);
                await unitOfWork.Employers.AddAsync(profile.Employer, cancellationToken);
            }
            else
            {
                profile.Employer.CompanyName = request.CompanyName;
                profile.Employer.CompanySizeId = request.CompanySizeId;
                profile.Employer.CompanyDescription = request.CompanyDescription;
                profile.Employer.CompanyWebsite = request.CompanyWebsite;
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
