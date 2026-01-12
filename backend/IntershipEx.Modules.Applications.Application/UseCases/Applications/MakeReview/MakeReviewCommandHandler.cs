using IntershipEx.Modules.Applications.Application.Abstractions.Data;
using IntershipEx.Modules.Applications.Domain.Applications;
using Modules.Common.Application.Messaging;
using Modules.Common.Domain.Abstractions;

namespace IntershipEx.Modules.Applications.Application.UseCases.Applications.MakeReview
{
    internal class MakeReviewCommandHandler(
        IUnitOfWork unitOfWork) : ICommandHandler<MakeReviewCommand>
    {
        public async Task<Result> Handle(MakeReviewCommand request, CancellationToken cancellationToken)
        {
            var application = await unitOfWork.Applications.GetByIdAsync(request.ApplicationId);
            if(application == null)
            {
                return Result.Failure(ApplicationErrors.NotFound);
            }
            var result = application.MakeReview(request.ReviewNotes);
            if(result.IsFailure)
            {
                return Result.Failure(result.Error);
            }   
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
