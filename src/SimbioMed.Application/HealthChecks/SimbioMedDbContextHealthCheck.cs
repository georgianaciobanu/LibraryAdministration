using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using SimbioMed.EntityFrameworkCore;

namespace SimbioMed.HealthChecks
{
    public class SimbioMedDbContextHealthCheck : IHealthCheck
    {
        private readonly DatabaseCheckHelper _checkHelper;

        public SimbioMedDbContextHealthCheck(DatabaseCheckHelper checkHelper)
        {
            _checkHelper = checkHelper;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            if (_checkHelper.Exist("db"))
            {
                return Task.FromResult(HealthCheckResult.Healthy("SimbioMedDbContext connected to database."));
            }

            return Task.FromResult(HealthCheckResult.Unhealthy("SimbioMedDbContext could not connect to database"));
        }
    }
}
