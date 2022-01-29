using NUnit.Framework;
using Steeltoe.Common.HealthChecks;

namespace Services.Tests
{
    [TestFixture]
    public class ServicesHealthContributorTests
    {
        [Test]
        public void CheckHealthAsync_Should_Return_Healthy()
        {
            var servicesHealthCheck = new ServicesHealthContributor();

            var healthCheckStatus = servicesHealthCheck.Health();

            Assert.That(healthCheckStatus.Status, Is.EqualTo(HealthStatus.UP));
        }
    }
}
