using Microsoft.Extensions.Diagnostics.HealthChecks;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Services.Tests
{
    [TestFixture]
    public class ServicesHealthCheckTests
    {
        [Test]
        public async Task CheckHealthAsync_Should_Return_Healthy()
        {
            var servicesHealthCheck = new ServicesHealthCheck();

            var healthCheckStatus = await servicesHealthCheck.CheckHealthAsync(null);

            Assert.That(healthCheckStatus, Is.EqualTo(HealthCheckResult.Healthy()));
        }
    }
}
