using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Users.Api.Persistence;
using Users.Api.Persistence.Models;

namespace Users.Api.Controllers
{
    [ApiController]
    public class MigrationsController : ControllerBase
    {
        private readonly UsersDbContext _context;

        public MigrationsController(UsersDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Updates the database to a target migration. Updates to latest version when no target migration provided.
        /// </summary>
        /// <param name="targetMigration">Target migration (optional).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        [HttpPost]
        [Route("/migrations/update-database")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateAsync(string? targetMigration = null, CancellationToken cancellationToken = default)
        {
            var migrator = _context.Database.GetService<IMigrator>();
            await migrator.MigrateAsync(targetMigration, cancellationToken);
            return NoContent();
        }

        /// <summary>
        /// Seed the initial data
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        [HttpPost]
        [Route("/migrations/seed-data")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> SeedAsync(CancellationToken cancellationToken = default)
        {
            var users = new List<User>
            {
                new User { FirstName = "Thijs", LastName= "Dickmans" },
            };
            _context.Users.AddRange(users);
            await _context.SaveChangesAsync(cancellationToken);
            return NoContent();
        }
    }
}