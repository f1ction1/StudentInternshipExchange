using Modules.Common.Application.Messaging;

namespace InternshipEx.Modules.Practices.Application.UseCases.Internships.RemoveInternshipFromFavorite
{
    public record RemoveInternshipFromFavoriteCommand(Guid InternshipId) : ICommand;
}
