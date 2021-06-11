using Microsoft.EntityFrameworkCore;
using TimeEntries.Api.Persistence.Models;

namespace TimeEntries.Api.Persistence
{
    public class TimeEntriesDbContext : DbContext
    {
        public DbSet<TimeEntry> TimeEntries { get; set; } = null!;

        public TimeEntriesDbContext(DbContextOptions<TimeEntriesDbContext> options)
            : base(options)
        {
        }
    }
}