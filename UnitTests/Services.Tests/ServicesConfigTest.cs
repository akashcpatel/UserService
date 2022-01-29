using NUnit.Framework;

namespace Services.Tests
{
    public class ServicesConfigTest
    {
        [Test]
        public void PositionInConfig_Should_Be_Set()
        {
            Assert.That(ServicesConfig.PositionInConfig, Is.EqualTo("Services"));
        }
    }
}
