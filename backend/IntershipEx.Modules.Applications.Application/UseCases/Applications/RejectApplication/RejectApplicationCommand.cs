using Modules.Common.Application.Messaging;

namespace IntershipEx.Modules.Applications.Application.UseCases.Applications.RejectApplication
{
    public record RejectApplicationCommand(Guid ApplicationId, string? RejectionReason) : ICommand;
}
