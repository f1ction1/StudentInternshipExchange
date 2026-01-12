using InternshipEx.Modules.Users.Application.Interfaces;
using InternshipEx.Modules.Users.Domain.Entities;
using Modules.Common.Application;
using MediatR;

namespace InternshipEx.Modules.Users.Application.UseCases.UpsertProfile
{
    public class UpsertProfileCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService) : IRequestHandler<UpsertProfileCommand>
    {
        public async Task Handle(UpsertProfileCommand request, CancellationToken cancellationToken)
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
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
