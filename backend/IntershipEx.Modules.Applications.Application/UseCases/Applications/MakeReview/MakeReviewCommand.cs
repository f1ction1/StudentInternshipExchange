using Modules.Common.Application.Messaging;

namespace IntershipEx.Modules.Applications.Application.UseCases.Applications.MakeReview
{
    public record MakeReviewCommand(
        Guid ApplicationId,
        string? ReviewNotes) : ICommand;
}
