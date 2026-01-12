using Modules.Common.Application.Messaging;
using Modules.Common.Domain.Abstractions;

namespace InternshipEx.Recommendations.Applications.UseCases.Interactions.AddInteraction
{
    internal class AddInteractionCommandHandler(
        ) : ICommandHandler<AddInteractionCommand>
    {
        public async Task<Result> Handle(AddInteractionCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
