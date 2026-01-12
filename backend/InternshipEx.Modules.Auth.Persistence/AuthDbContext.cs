using InternshipEx.Modules.Auth.Domain.Entities;
using InternshipEx.Modules.Auth.Persistence.Outbox;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace InternshipEx.Modules.Auth.Persistence
{
    public class AuthDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<OutboxMessage> OutboxMessages { get; set; }

        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("auth");
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
        }
    }
}
