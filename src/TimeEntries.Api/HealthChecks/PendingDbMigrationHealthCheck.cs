using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace TimeEntries.Api.HealthChecks
{
    public class PendingDbMigrationHealthCheck<TContext> : IHealthCheck
        where TContext : DbContext
    {
        private readonly TContext _dbContext;

        public PendingDbMigrationHealthCheck(TContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = default)
        {
            IEnumerable<string> pending = await _dbContext.Database
                .GetPendingMigrationsAsync(cancellationToken);
            string[] migrations = pending as string[] ?? pending.ToArray();
            var isHealthy = !migrations.Any();

            return isHealthy
                ? HealthCheckResult.Healthy("No pending db migrations")
                : HealthCheckResult.Unhealthy($"{migrations.Length} migrations pending!");
        }
    }
}