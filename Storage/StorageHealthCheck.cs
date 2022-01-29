using Steeltoe.Common.HealthChecks;

namespace Storage
{
    internal class StorageHealthContributor : IHealthContributor
    {
        public string Id => nameof(StorageHealthContributor);

        public HealthCheckResult Health()
        {
            return new HealthCheckResult
            {
                Description = HealthStatus.UP.ToString(),
                Status = HealthStatus.UP
            };
        }
    }
}
