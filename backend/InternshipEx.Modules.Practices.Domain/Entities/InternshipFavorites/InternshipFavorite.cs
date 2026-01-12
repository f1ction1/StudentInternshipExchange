namespace InternshipEx.Modules.Practices.Domain.Entities.InternshipFavorites
{
    public class InternshipFavorite
    {
        public Guid StudentId { get; set; } 
        public Guid InternshipId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public virtual Internship Internship { get; set; } = null!;

        public static InternshipFavorite Create(Guid studentId, Guid internshipId)
        {
            var entity = new InternshipFavorite
            {
                StudentId = studentId,
                InternshipId = internshipId,
                CreatedAt = DateTime.UtcNow
            };
            //here should be added event, but tu speed up the process i did it directly in the command handler
            return entity;
        }
    }
}
