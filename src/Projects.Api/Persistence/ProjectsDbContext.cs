using Microsoft.EntityFrameworkCore;
using Projects.Api.Persistence.Models;

namespace Projects.Api.Persistence
{
    public class ProjectsDbContext : DbContext
    {
        public DbSet<Project> Projects { get; set; } = null!;

        public ProjectsDbContext(DbContextOptions<ProjectsDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contributor>()
                .HasOne(p => p.Project)
                .WithMany(b => b.Contributors)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}