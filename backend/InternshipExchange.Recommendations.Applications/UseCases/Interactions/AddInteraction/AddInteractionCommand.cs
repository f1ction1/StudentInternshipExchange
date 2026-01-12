using Modules.Common.Application.Messaging;

namespace InternshipEx.Recommendations.Applications.UseCases.Interactions.AddInteraction
{
    public record AddInteractionCommand(string Type) : ICommand;
}
