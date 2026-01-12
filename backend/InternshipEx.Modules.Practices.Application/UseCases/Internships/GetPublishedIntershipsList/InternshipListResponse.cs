namespace InternshipEx.Modules.Practices.Application.UseCases.Internships.GetIntershipsList
{
    public class InternshipListResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string EmployerName { get; set; } = string.Empty;
        public string CityName { get; set; } = string.Empty;
        public string CountryName { get; set; } = string.Empty;
        public bool IsRemote { get; set; }
        public bool IsLiked { get; set; }
        public string CreatedAt { get; set; } = string.Empty;
    }
}
