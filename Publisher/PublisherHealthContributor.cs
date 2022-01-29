using Steeltoe.Common.HealthChecks;

namespace Publisher
{
    internal class PublisherHealthContributor : IHealthContributor
    {
        public string Id => nameof(PublisherHealthContributor);

        public HealthCheckResult Health()
        {
            return new HealthCheckResult
            {
                Status = HealthStatus.UP,
                Description = HealthStatus.UP.ToString()
            };
        }
    }
}
