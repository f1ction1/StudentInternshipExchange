using InternshipEx.Modules.Users.Application.Interfaces;
using Modules.Common.Application.Exceptions;
using MediatR;

namespace InternshipEx.Modules.Users.Application.UseCases.IsCompleteEmployerProfile
{
    public class IsCompleteEmployerProfileCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<IsCompleteEmployerProfileCommand, bool>
    {
        public async Task<bool> Handle(IsCompleteEmployerProfileCommand request, CancellationToken cancellationToken)
        {
            var profileId = request.id;
            var profile = await unitOfWork.Profiles.GetByIdAsync(profileId);
            if (profile is null )
            {
                throw new NotFoundException("Can't find profile with given id");
            }
            return profile.IsProfileComplete("employer");
        }
    }
}
