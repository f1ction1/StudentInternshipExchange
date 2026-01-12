namespace InternshipEx.Modules.Users.Application.Interfaces
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync(CancellationToken ct = default);

        public IProfileRepository Profiles { get; }
        public IStudentRepository Students { get; }
        public ICvRepository Cvs { get; }
        public IEmployerRepository Employers { get; }
    }
}
