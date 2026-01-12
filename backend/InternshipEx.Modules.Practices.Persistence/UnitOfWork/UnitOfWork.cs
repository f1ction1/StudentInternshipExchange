using InternshipEx.Modules.Practices.Application.Interfaces;
using InternshipEx.Modules.Practices.Domain.Entities;
using InternshipEx.Modules.Practices.Persistence.Repositories;

namespace InternshipEx.Modules.Practices.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PracticesDbContext _context;
        private DictionaryRepository<Skill>? _skillDictionaryRepository;
        private DictionaryRepository<City>? _cityDictionaryRepository;
        private DictionaryRepository<Industry>? _industryDictionaryRepository;
        private DictionaryRepository<Country>? _countryDictionaryRepository;
        private InternshipRepository? _internshipRepository;
        private InternshipFavoriteRepository? _internshipFavoriteRepository;

        public UnitOfWork(PracticesDbContext context)
        {
            _context = context;
        }

        public PracticesDbContext Context => _context;
        public IDictionaryRepository<Skill> Skills => _skillDictionaryRepository ??= new DictionaryRepository<Skill>(Context);
        public IDictionaryRepository<City> Cities => _cityDictionaryRepository ??= new DictionaryRepository<City>(Context);
        public IDictionaryRepository<Industry> Industries => _industryDictionaryRepository ??= new DictionaryRepository<Industry>(Context);
        public IDictionaryRepository<Country> Countries => _countryDictionaryRepository ??= new DictionaryRepository<Country>(Context);
        public IInternshipRepository Internships => _internshipRepository ??= new InternshipRepository(Context);
        public IInternshipFavoriteRepository InternshipFavorites => _internshipFavoriteRepository ??= new InternshipFavoriteRepository(Context);

        public async Task SaveChangesAsync(CancellationToken ct = default)
        {
            await _context.SaveChangesAsync(ct);
        }
    }
}
