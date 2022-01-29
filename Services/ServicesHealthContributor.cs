using Steeltoe.Common.HealthChecks;

namespace Services
{
    internal class ServicesHealthContributor : IHealthContributor
    {
        public string Id => nameof(ServicesHealthContributor);

        public Steeltoe.Common.HealthChecks.HealthCheckResult Health()
        {
            return new HealthCheckResult
            {
                Status = HealthStatus.UP,
                Description = HealthStatus.UP.ToString()
            };
        }
    }
}
