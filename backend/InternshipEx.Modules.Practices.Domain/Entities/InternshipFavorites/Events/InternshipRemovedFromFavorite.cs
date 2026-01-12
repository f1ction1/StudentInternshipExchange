namespace InternshipEx.Modules.Practices.Domain.Entities.InternshipFavorites.Events
{
    public record InternshipRemovedFromFavorite(Guid EventId, Guid InternshipId, Guid StudentId);
}
