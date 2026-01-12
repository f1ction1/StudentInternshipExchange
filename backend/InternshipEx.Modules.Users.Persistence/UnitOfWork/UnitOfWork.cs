using InternshipEx.Modules.Users.Application.Interfaces;
using InternshipEx.Modules.Users.Persistence.Repositories;

namespace InternshipEx.Modules.Users.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly UsersDbContext _context;
        private ProfileRepository? _profileRepository;
        private StudentRepository? _studentRepository;
        private CvRepository? _cvRepository;
        private EmployerRepository? _employerRepository;
        public UnitOfWork(UsersDbContext context)
        {
            _context = context;
        }

        public UsersDbContext Context => _context;
        public IProfileRepository Profiles => _profileRepository ??= new ProfileRepository(Context);
        public IStudentRepository Students => _studentRepository ??= new StudentRepository(Context);
        public ICvRepository Cvs => _cvRepository ??= new CvRepository(Context);
        public IEmployerRepository Employers => _employerRepository ??= new EmployerRepository(Context);

        public async Task SaveChangesAsync(CancellationToken ct = default)
        {
            await _context.SaveChangesAsync(ct);
        }
    }
}
