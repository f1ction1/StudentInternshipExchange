using IntershipEx.Modules.Applications.Application.Abstractions.Messaging;

namespace IntershipEx.Modules.Applications.Application.UseCases.Applications.ApplyInternship
{
    public record ApplyInternshipCommand(
        Guid InternshipId,
        string CoverLetter) : ICommand;
}
