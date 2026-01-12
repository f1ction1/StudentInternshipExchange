using InternshipEx.Recommendations.Domain.Interactions;
using Microsoft.EntityFrameworkCore;

namespace InternshipEx.Recommendations.Persistence
{
    public class RecDbContext : DbContext
    {
        public DbSet<Interaction> Interactions { get; set; }

        public RecDbContext(DbContextOptions<RecDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasDefaultSchema("recommendations");
        }
    }
}
