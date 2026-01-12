using InternshipEx.Modules.Practices.Domain.DTOs.Dictionaries;

namespace InternshipEx.Modules.Practices.Domain.DTOs
{
    public class GetEmployerInternshipsDto
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public bool IsRemote { get; set; }
        public DictionaryDto? Country { get; set; }
        public DictionaryDto? City { get; set; }
        public DictionaryDto? Industry { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ExpiresAt { get; set; }
        public string? InternshipStatus { get; set; }
        public IEnumerable<DictionaryDto>? Skills { get; set; }
    }
}
