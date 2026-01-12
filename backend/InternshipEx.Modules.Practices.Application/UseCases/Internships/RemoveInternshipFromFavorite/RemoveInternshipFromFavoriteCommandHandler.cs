using InternshipEx.Modules.Practices.Application.Interfaces;
using InternshipEx.Modules.Practices.Domain.Entities.InternshipFavorites.Events;
using Modules.Common.Application;
using MassTransit;
using Modules.Common.Application.Messaging;
using Modules.Common.Domain.Abstractions;

namespace InternshipEx.Modules.Practices.Application.UseCases.Internships.RemoveInternshipFromFavorite
{
    internal class RemoveInternshipFromFavoriteCommandHandler(
        IUnitOfWork unitOfWork,
        ICurrentUserService currentUserService,
        IPublishEndpoint publishEndpoint) : ICommandHandler<RemoveInternshipFromFavoriteCommand>
    {
        public async Task<Result> Handle(RemoveInternshipFromFavoriteCommand request, CancellationToken cancellationToken)
        {
            var internshipId = request.InternshipId;
            var studentId = currentUserService.UserId;
            var existingFavoriteRecord = await unitOfWork.InternshipFavorites.GetFavoriteAsync(internshipId, studentId);
            if (existingFavoriteRecord is null)
            {
                return Result.Success();
            }
            await unitOfWork.InternshipFavorites.RemoveFavoriteAsync(existingFavoriteRecord);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            await publishEndpoint.Publish(new InternshipRemovedFromFavorite(Guid.NewGuid(), internshipId, studentId), cancellationToken);
            return Result.Success();
        }
    }
}
