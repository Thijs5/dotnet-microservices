using Microsoft.EntityFrameworkCore;
using Users.Api.Persistence.Models;

namespace Users.Api.Persistence
{
    public class UsersDbContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;

        public UsersDbContext(DbContextOptions<UsersDbContext> options)
            : base(options)
        {
        }
    }
}