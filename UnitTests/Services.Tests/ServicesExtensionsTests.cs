using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

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

        //[Test]
        //public void Test_AddServices()
        //{
        //    _servicesMock.Setup(s => s.AddHealthChecks()).Returns(_healthCheckBuilderMock.Object);

        //    _servicesMock.Object.AddServices(_configurationMock.Object);
        //}
    }
}
