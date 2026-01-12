namespace InternshipEx.Modules.Practices.Domain.Entities
{
    public class Industry
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public virtual ICollection<Internship> Internships { get; set; } = new List<Internship>();
    }
}
