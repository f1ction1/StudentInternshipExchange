using InternshipEx.Modules.Practices.Domain.Entities.InternshipFavorites;
using InternshipEx.Modules.Practices.Domain.Enums;

namespace InternshipEx.Modules.Practices.Domain.Entities
{
    public class Internship : Entity<Guid>
    {
        public Guid EmployerId { get; set; } // Profile.Id з Users Service

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public int CityId { get; set; }
        public virtual City City { get; set; } = null!;
        public int CountryId { get; set; }
        public virtual Country Country { get; set; } = null!;
        public bool IsRemote { get; set; } = false;

        public int IndustryId { get; set; }
        public virtual Industry Industry { get; set; } = null!;

        public bool IsPublished { get; set; } = true;
        public DateTime CreatedAt { get; set; } 
        public DateTime ExpiresAt { get; set; }

        public InternshipStatus InternshipStatus { get; set; }
        public int ApplicationsCount { get; private set; }

        public string EmployerName { get; set; } = string.Empty;
        public string? EmployerLogoUrl { get; set; }

        public virtual ICollection<Skill> Skills { get; set; } = new List<Skill>();
        public virtual ICollection<InternshipFavorite> Favorites { get; set; } = new List<InternshipFavorite>();

        public static Internship Create(
            Guid employerId,
            string title,
            string description,
            int cityId,
            int countryId,
            bool isRemote,
            int industryId,
            ICollection<Skill> skills,
            DateTime expiresAt,
            string employerName,
            string? employerLogoUrl)
        {
            return new Internship
            {
                Id = Guid.NewGuid(),
                EmployerId = employerId,
                Title = title,
                Description = description,
                CityId = cityId,
                CountryId = countryId,
                IsRemote = isRemote,
                IndustryId = industryId,
                Skills = skills,
                IsPublished = true,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = expiresAt,
                EmployerName = employerName,
                EmployerLogoUrl = employerLogoUrl,
                InternshipStatus = InternshipStatus.Active,
                ApplicationsCount = 0,
            };
        }
        
        public void Patch(
            string? title,
            string? description,
            int? cityId,
            int? countryId,
            bool? isRemote,
            int? industryId,
            ICollection<Skill>? skills,
            DateTime? expiresAt)
        {
            if (title is not null)
                Title = title;

            if (description is not null)
                Description = description;

            if (cityId.HasValue)
                CityId = cityId.Value;

            if (countryId.HasValue)
                CountryId = countryId.Value;

            if (isRemote.HasValue)
                IsRemote = isRemote.Value;

            if (industryId.HasValue)
                IndustryId = industryId.Value;

            if (skills is not null)
            {
                Skills.Clear();
                foreach (var skill in skills)
                    Skills.Add(skill);
            }

            if (expiresAt.HasValue)
                ExpiresAt = expiresAt.Value;
        }
    }
}
