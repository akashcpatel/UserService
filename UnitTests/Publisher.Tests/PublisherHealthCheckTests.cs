using Microsoft.Extensions.Diagnostics.HealthChecks;
using NUnit.Framework;
using Publisher;
using System.Threading.Tasks;

namespace Services.Tests
{
    [TestFixture]
    public class PublisherHealthCheckTests
    {
        [Test]
        public async Task CheckHealthAsync_Should_Return_Healthy()
        {
            var publisherHealthCheck = new PublisherHealthCheck();

            var healthCheckStatus = await publisherHealthCheck.CheckHealthAsync(null);

            Assert.That(healthCheckStatus, Is.EqualTo(HealthCheckResult.Healthy()));
        }
    }
}
