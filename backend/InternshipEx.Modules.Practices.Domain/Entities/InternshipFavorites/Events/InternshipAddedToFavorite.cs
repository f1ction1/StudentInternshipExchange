namespace InternshipEx.Modules.Practices.Domain.Entities.InternshipFavorites.Events
{
    public record InternshipAddedToFavorite(Guid EventId, Guid InternshipId, Guid StudentId, DateTime TimeStamp);
}
