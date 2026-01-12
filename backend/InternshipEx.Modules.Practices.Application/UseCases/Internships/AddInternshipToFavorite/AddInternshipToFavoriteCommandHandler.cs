using InternshipEx.Modules.Practices.Application.Interfaces;
using InternshipEx.Modules.Practices.Domain.Entities.InternshipFavorites;
using InternshipEx.Modules.Practices.Domain.Entities.InternshipFavorites.Events;
using InternshipEx.Modules.Practices.Domain.Entities.Internships;
using Modules.Common.Application;
using MassTransit;
using Modules.Common.Application.Messaging;
using Modules.Common.Domain.Abstractions;

namespace InternshipEx.Modules.Practices.Application.UseCases.Internships.AddInternshipToFavorite
{
    internal class AddInternshipToFavoriteCommandHandler(
        IUnitOfWork unitOfWork,
        ICurrentUserService currentUserService,
        IPublishEndpoint publishEndpoint) : ICommandHandler<AddInternshipToFavoriteCommand>
    {
        public async Task<Result> Handle(AddInternshipToFavoriteCommand request, CancellationToken cancellationToken)
        {
            var internshipId = request.InternshipId;
            var internship = await unitOfWork.Internships.GetByIdAsync(internshipId);
            if(internship is null)
            {
                return Result.Failure(InternshipErrors.NotFound);
            }
            var studentId = currentUserService.UserId;
            var alreadyFavorited = await unitOfWork.InternshipFavorites.GetFavoriteAsync(internshipId, studentId);
            if(alreadyFavorited is not null)
            {
                return Result.Success();
            }
            var internshipFavorite = InternshipFavorite.Create(studentId, internshipId);
            await unitOfWork.InternshipFavorites.AddFavoriteAsync(internshipFavorite);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            await publishEndpoint.Publish(new InternshipAddedToFavorite(Guid.NewGuid(), internshipId, studentId,internshipFavorite.CreatedAt), cancellationToken);
            return Result.Success();
        }
    }
}
