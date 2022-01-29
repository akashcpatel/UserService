using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace Services.Tests
{
    [TestFixture]
    public class ServicesExtensionsTests
    {
        private Mock<IServiceCollection> _servicesMock;
        private Mock<IConfiguration> _configurationMock;
        private Mock<IHealthChecksBuilder> _healthCheckBuilderMock; 

        [SetUp]
        public void Setup()
        {
            _servicesMock = new Mock<IServiceCollection>();
            _configurationMock = new Mock<IConfiguration>();
            _healthCheckBuilderMock = new Mock<IHealthChecksBuilder>();
        }
    }
}
