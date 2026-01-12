using IntershipEx.Modules.Applications.Application.Abstractions.Data;
using IntershipEx.Modules.Applications.Domain.Applications;
using Modules.Common.Application.Messaging;
using Modules.Common.Domain.Abstractions;

namespace IntershipEx.Modules.Applications.Application.UseCases.Applications.RejectApplication
{
    internal class RejectApplicationCommandHandler(
        IUnitOfWork unitOfWork) : ICommandHandler<RejectApplicationCommand>
    {
        public async Task<Result> Handle(RejectApplicationCommand request, CancellationToken cancellationToken)
        {
            var application = await unitOfWork.Applications.GetByIdAsync(request.ApplicationId);
            if (application == null) 
            { 
                return Result.Failure(ApplicationErrors.NotFound);
            }
            application.RejectApplication(request.RejectionReason);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
