namespace InternshipEx.Recommendations.Infrastructure.Test
{
    public class InternshipRecommendationDto
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? EmployerName { get; set; }
        public string? CityName { get; set; }
        public string? CountryName { get; set; }
        public bool IsRemote { get; set; }
        public double Score { get; set; }
    }
}
