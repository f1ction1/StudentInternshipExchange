namespace InternshipEx.Modules.Practices.Domain.Entities
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public int CountryId { get; set; }
        public virtual Country Country { get; set; } = null!;
        public virtual ICollection<Internship> Internships { get; set; } = new List<Internship>();
    }
}
