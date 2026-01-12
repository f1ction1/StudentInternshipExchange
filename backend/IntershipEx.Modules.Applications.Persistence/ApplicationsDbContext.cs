using Microsoft.EntityFrameworkCore;
using ApplicationEntity = IntershipEx.Modules.Applications.Domain.Applications.Application;

namespace IntershipEx.Modules.Applications.Persistence;

public class ApplicationsDbContext : DbContext
{
    public DbSet<ApplicationEntity> Applications { get; set; }
    public ApplicationsDbContext(DbContextOptions<ApplicationsDbContext> options)
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.HasDefaultSchema("applications");

        builder.Entity<ApplicationEntity>(b =>
        {
            b.HasKey(application => application.Id);
            b.OwnsOne(application => application.Review);
            b.OwnsOne(application => application.Reject);
            b.OwnsOne(application => application.InternshipInfo);
        });
    }
}
