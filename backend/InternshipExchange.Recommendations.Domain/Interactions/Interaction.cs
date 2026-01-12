using Modules.Common.Domain.Abstractions;

namespace InternshipEx.Recommendations.Domain.Interactions
{
    public class Interaction : Entity<Guid>
    {
        public Guid? EventId { get; private set; }
        public Guid UserId { get; private set; } // Student
        public Guid InternshipId { get; private set; }
        public InteractionType Type { get; private set; }
        public DateTime TimeStamp { get; private set; }

        public Interaction() : base() { }
        public static Interaction Create(Guid userId, Guid internshipId, InteractionType type, DateTime timeStamp, Guid? eventId = null)
        {
            return new Interaction
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                InternshipId = internshipId,
                Type = type,
                TimeStamp = timeStamp,
                EventId = eventId
            };
        }
    }
}
