using InternshipEx.Modules.Users.Application.Interfaces;
using InternshipEx.Modules.Users.Domain.DTOs;
using InternshipEx.Modules.Users.Domain.Entities.Profiles;
using Modules.Common.Application;
using Modules.Common.Application.Exceptions;
using MediatR;
using Modules.Common.Application.Messaging;
using Modules.Common.Domain.Abstractions;

namespace InternshipEx.Modules.Users.Application.UseCases.GetProfile
{
    public class GetProfileQueryHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService) : IQueryHandler<GetProfileQuery, ProfileDto>
    {
        public async Task<Result<ProfileDto>> Handle(GetProfileQuery request, CancellationToken cancellationToken)
        {
            var id = currentUserService.UserId;

            var profile = await unitOfWork.Profiles.GetByIdAsync(id, cancellationToken);
            if (profile is null)
                return Result.Failure<ProfileDto>(ProfileErrors.NotFoundOrEmpty);

            return new ProfileDto
            {
                FirstName = profile.FirstName!,
                LastName = profile.LastName!,
                PhoneNumber = profile.PhoneNumber!,
            };
        }
    }
}
