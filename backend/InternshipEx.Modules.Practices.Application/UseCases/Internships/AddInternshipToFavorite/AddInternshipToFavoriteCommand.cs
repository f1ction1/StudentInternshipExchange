using Modules.Common.Application.Messaging;

namespace InternshipEx.Modules.Practices.Application.UseCases.Internships.AddInternshipToFavorite
{
    public record AddInternshipToFavoriteCommand(Guid InternshipId) : ICommand;
}
