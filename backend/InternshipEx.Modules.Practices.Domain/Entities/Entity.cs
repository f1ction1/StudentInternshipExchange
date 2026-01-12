namespace InternshipEx.Modules.Practices.Domain.Entities
{
    public class Entity<TId> where TId : struct
    {
        public TId Id { get; set; }
    }
}
