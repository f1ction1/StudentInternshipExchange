using InternshipEx.Modules.Practices.Domain.Entities;

namespace InternshipEx.Modules.Practices.Domain.DTOs
{
    public class AddInternshipDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsRemote { get; set; }
        public int CountryId { get; set; }
        public int CityId { get; set; }
        public int IndustryId { get; set; }
        public DateTime ExpiresAt { get; set; }
        public List<int> SkillsIds { get; set; } = new List<int>();
    }
}
