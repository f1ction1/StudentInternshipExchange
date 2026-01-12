using InternshipEx.Modules.Users.Application.Interfaces;
using InternshipEx.Modules.Users.Domain.Entities.Profiles;
using Modules.Common.Application;
using Modules.Common.Application.Messaging;
using Modules.Common.Domain.Abstractions;

namespace InternshipEx.Modules.Users.Application.UseCases.IsCompleteProfile
{
    internal class IsCompleteProfileQueryHandler(
        IUnitOfWork unitOfWork,
        ICurrentUserService currentUserService) : IQueryHandler<IsCompleteProfileQuery, bool>
    {
        public async Task<Result<bool>> Handle(IsCompleteProfileQuery request, CancellationToken cancellationToken)
        {
            var profileId = currentUserService.UserId;
            var profile = await unitOfWork.Profiles.GetByIdAsync(profileId, cancellationToken);
            if(profile is null)
            {
                return Result.Failure<bool>(ProfileErrors.NotFound);
            }
            var isComplete = profile.IsProfileComplete(currentUserService.Role);
            return Result.Success(isComplete);
        }
    }
}
