using InternshipEx.Modules.Practices.Domain.Entities;

namespace InternshipEx.Modules.Practices.Domain.DTOs
{
    public class PatchInternshipDto
    {
        public Guid Id { get; set; }
        public string? Title { get; set; } = null;
        public string? Description { get; set; } = null;
        public bool? IsRemote { get; set; } = null;
        public int? CountryId { get; set; } = null;
        public int? CityId { get; set; } = null;
        public int? IndustryId { get; set; } = null;
        public DateTime? ExpiresAt { get; set; } = null;
        public List<int>? SkillsIds { get; set; } = null;
    }
}
