using InternshipEx.Modules.Practices.Domain.Entities;
using InternshipEx.Modules.Practices.Domain.Entities.InternshipFavorites;
using Microsoft.EntityFrameworkCore;

namespace InternshipEx.Modules.Practices.Persistence
{
    public class PracticesDbContext : DbContext
    {
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Industry> Industries { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Internship> Internships { get; set; }
        public DbSet<InternshipFavorite> InternshipFavorites { get; set; }

        public PracticesDbContext(DbContextOptions<PracticesDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasDefaultSchema("interships");

            // Country -> City
            builder.Entity<Country>()
                .HasMany(c => c.Cities)
                .WithOne(c => c.Country)
                .HasForeignKey(c => c.CountryId)
                .OnDelete(DeleteBehavior.Restrict);

            // City -> Internships
            builder.Entity<City>()
                .HasMany(c => c.Internships)
                .WithOne(p => p.City)
                .HasForeignKey(p => p.CityId)
                .OnDelete(DeleteBehavior.Restrict);

            // Industry -> Internships
            builder.Entity<Industry>()
                .HasMany(i => i.Internships)
                .WithOne(p => p.Industry)
                .HasForeignKey(p => p.IndustryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Practice <-> Skill (many-to-many)
            builder.Entity<Internship>()
                .HasMany(p => p.Skills)
                .WithMany(s => s.Internships)
                .UsingEntity(j => j.ToTable("InternshipSkills"));

            // Practice -> Favorites
            builder.Entity<InternshipFavorite>()
                .HasKey(f => new { f.InternshipId, f.StudentId });

            builder.Entity<InternshipFavorite>()
                .HasOne(f => f.Internship)
                .WithMany(p => p.Favorites)
                .HasForeignKey(f => f.InternshipId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
