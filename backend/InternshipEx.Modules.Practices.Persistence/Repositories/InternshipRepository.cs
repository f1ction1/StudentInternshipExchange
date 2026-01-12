using InternshipEx.Modules.Practices.Application.DTOs.Internship;
using InternshipEx.Modules.Practices.Application.Interfaces;
using InternshipEx.Modules.Practices.Application.UseCases.Internships.GetIntershipsList;
using InternshipEx.Modules.Practices.Application.UseCases.Internships.GetPublishedIntershipsList;
using InternshipEx.Modules.Practices.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace InternshipEx.Modules.Practices.Persistence.Repositories
{
    public class InternshipRepository : Repository<Internship, Guid>, IInternshipRepository
    {
        public InternshipRepository(PracticesDbContext context) : base(context)
        { }

        public async Task<IReadOnlyCollection<Internship>> GetAll()
        {
            return await _dbSet
                .Include(i => i.City)
                .Include(i => i.Country)
                .Include(i => i.Industry)
                .Include(i => i.Skills)
                .ToListAsync();
        }

        public async Task<IList<Internship>> GetInternshipsByEmployer(Guid employerId)
        {
            return await _dbSet.Where(internship => internship.EmployerId == employerId)
                .Include(i => i.City)
                .Include(i => i.Country)
                .Include(i => i.Industry)
                .Include(i => i.Skills)
                .ToListAsync();
        }

        public override async Task<Internship?> GetByIdAsync(Guid id)
        {
            return await _dbSet
                .Include(i => i.Skills)
                .Include(i => i.City)
                .Include(i => i.Country)
                .SingleOrDefaultAsync(e => e.Id.Equals(id));
        }
        public async Task<IPagedList<PublishedInternshipDto>> GetPublishedInternshipsAsync(GetPublishedInternshipsListQuery query, Guid studentId)
        {
            
            // Start with base query for published and active internships
            var queryable = _dbSet
                .Include(i => i.City)
                .Include(i => i.Country)
                .Include(i => i.Industry)
                .Include(i => i.Skills)
                .Include(i => i.Favorites)
                .Where(i => i.IsPublished && i.InternshipStatus == Domain.Enums.InternshipStatus.Active)
                .AsQueryable();

            // Apply filters
            queryable = ApplyFilters(queryable, query);

            // Apply sorting
            //queryable = ApplySorting(queryable, query);

            var internshipsQuery = queryable
                .Select(i => new PublishedInternshipDto
                {
                    Id = i.Id,
                    Title = i.Title,
                    EmployerName = i.EmployerName,
                    EmployerLogoUrl = i.EmployerLogoUrl,
                    CityName = i.City.Name,
                    CountryName = i.Country.Name,
                    IsLiked = studentId != Guid.Empty ? i.Favorites.Any(f => f.InternshipId == i.Id && f.StudentId == studentId) : false,
                    IsRemote = i.IsRemote,
                    IndustryName = i.Industry.Name,
                    CreatedAt = i.CreatedAt,
                    Skills = i.Skills.Select(s => s.Name).ToList()
                });

            var internships = await PagedList<PublishedInternshipDto>.CreateAsync(internshipsQuery, query.Page, query.PageSize);

            return internships;
        }

        private IQueryable<Internship> ApplyFilters(
            IQueryable<Internship> queryable,
            GetPublishedInternshipsListQuery query)
        {
            // Search term filter (searches in title, description, company name)
            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                var searchTerm = query.SearchTerm.ToLower();
                queryable = queryable.Where(i =>
                    i.Title.ToLower().Contains(searchTerm) ||
                    i.Description.ToLower().Contains(searchTerm) ||
                    i.EmployerName.ToLower().Contains(searchTerm));
            }

            // Location filters
            if (query.CountryId.HasValue)
            {
                queryable = queryable.Where(i => i.CountryId == query.CountryId.Value);
                if (query.CityId.HasValue)
                {
                    queryable = queryable.Where(i => i.CityId == query.CityId.Value);
                }
            }

            // Industry filter
            if (query.IndustryIds != null && query.IndustryIds.Any()) 
            {
                queryable = queryable.Where(i => query.IndustryIds.Contains(i.IndustryId));
            }

            // Remote filter
            if (query.IsRemote.HasValue)
            {
                queryable = queryable.Where(i => i.IsRemote == query.IsRemote.Value);
            }

            // Skills filter - internship must have at least one of the specified skills
            if (query.SkillIds != null && query.SkillIds.Any())
            {
                queryable = queryable.Where(i =>
                    i.Skills.Any(skill => query.SkillIds.Contains(skill.Id)));
            }

            // Date range filters
            //if (query.CreatedAfter.HasValue)
            //{
            //    queryable = queryable.Where(i => i.CreatedAt >= query.CreatedAfter.Value);
            //}

            //if (query.CreatedBefore.HasValue)
            //{
            //    queryable = queryable.Where(i => i.CreatedAt <= query.CreatedBefore.Value);
            //}

            //if (query.ExpiresAfter.HasValue)
            //{
            //    queryable = queryable.Where(i => i.ExpiresAt >= query.ExpiresAfter.Value);
            //}

            // Filter out expired internships
            queryable = queryable.Where(i => i.ExpiresAt > DateTime.UtcNow);

            return queryable;
        }

        public async Task<IList<Internship>> GetFavoriteInternshipsAsync(Guid studentId)
        {
            return await _context.InternshipFavorites
                .Include(i => i.Internship)
                .ThenInclude(i => i.City)
                .ThenInclude(i => i.Country)
                .Where(f => f.StudentId == studentId)
                .Select(f => f.Internship)
                .ToListAsync();
        }
    }
}
