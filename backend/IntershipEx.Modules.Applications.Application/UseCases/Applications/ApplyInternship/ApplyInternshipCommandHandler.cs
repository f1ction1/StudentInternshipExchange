using InternshipEx.Modules.Practices.Contracts;
using InternshipEx.Modules.Practices.Contracts.DTOs;
using InternshipEx.Modules.Users.Contracts;
using IntershipEx.Modules.Applications.Application.Abstractions.Data;
using IntershipEx.Modules.Applications.Application.Abstractions.Messaging;
using IntershipEx.Modules.Applications.Domain.Applications;
using MassTransit;
using Modules.Common.Application;
using Modules.Common.Domain.Abstractions;
using ApplicationEntity = IntershipEx.Modules.Applications.Domain.Applications.Application;

namespace IntershipEx.Modules.Applications.Application.UseCases.Applications.ApplyInternship;

internal sealed class ApplyInternshipCommandHandler(
    ICurrentUserService currentUserService,
    IUnitOfWork unitOfWork,
    IInternshipModuleApi internshipPublicApi,
    IUsersPublicApi usersPublicApi,
    IPublishEndpoint publishEndpoint) : ICommandHandler<ApplyInternshipCommand>
{
    public async Task<Result> Handle(ApplyInternshipCommand request, CancellationToken cancellationToken)
    {
        Result<InternshipDTO> internshipResult = await internshipPublicApi.GetInternship(request.InternshipId, cancellationToken);
        if(internshipResult.IsFailure)
        {
            return internshipResult;
        }
        var internshipDto = internshipResult.Value;

        var studentId = currentUserService.UserId;

        Result studentResult = await usersPublicApi.IsStudentCanApplyAsync(studentId, cancellationToken);
        if(studentResult.IsFailure)
        {
            return studentResult;
        }

        var applications = await unitOfWork.Applications.GetApplicationsByStudentAsync(studentId);
        var isExist = applications.Where(a => a.IntershipId == request.InternshipId);
        if(isExist.Any())
        {
            return Result.Failure(ApplicationErrors.AlreadyApplied);
        }
        //twotzy zgłoszenie i wysyła zdarzenie ApplicationApplied
        var application = ApplicationEntity.Create(
            request.InternshipId,
            studentId,
            request.CoverLetter,
            new InternshipInfo(
                internshipDto.Title,
                internshipDto.CompanyName,
                internshipDto.CompanyLocation
            )
        );
        await unitOfWork.Applications.Add(application);
        await unitOfWork.SaveChangesAsync();
        //should be used Outbox pattern to publish events, but to speed up proces we just get domain events here
        var domainEvents = application.GetDomainEvents();
        foreach(var domainEvent in domainEvents)
        {
            await publishEndpoint.Publish(domainEvent, domainEvent.GetType(), cancellationToken);
        }
        return Result.Success();
    }
}
