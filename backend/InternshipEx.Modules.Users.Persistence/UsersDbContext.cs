using InternshipEx.Modules.Users.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InternshipEx.Modules.Users.Persistence
{
    public class UsersDbContext : DbContext
    {
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Employer> Employers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<CompanySize> CompanySizes { get; set; }
        public DbSet<Cv> Cvs { get; set; }
        public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("users");

            modelBuilder.Entity<Cv>(b =>
            {
                b.HasKey(c => c.Id);
                b.HasOne(cv => cv.Student).WithOne(s => s.Cv)
                 .HasForeignKey<Cv>(s => s.StudentId)
                 .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Profile>(b =>
            {
                b.HasKey(p => p.Id);
                b.HasIndex(p => p.EmployerId);
                b.HasOne(p => p.Employer)
                 .WithMany(e => e.Profiles)
                 .HasForeignKey(p => p.EmployerId)
                 .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<Student>(b =>
            {
                b.HasKey(s => s.ProfileId); // PK = FK
                b.HasOne(s => s.Profile)
                 .WithOne(p => p.Student)
                 .HasForeignKey<Student>(s => s.ProfileId)
                 .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Employer>(b =>
            {
                b.HasKey(e => e.Id);
                b.HasOne(e => e.CompanySize).WithMany().HasForeignKey(e => e.CompanySizeId);
            });

            modelBuilder.Entity<CompanySize>(b =>
            {
                b.HasKey(c => c.Id);
            });
        }
    }
}
