using Model;
using Moq;
using NUnit.Framework;
using Publisher.Implementations;
using System;
using System.Threading.Tasks;

namespace Publisher.Tests.Implementations
{
    public class UserChangedPublisherTests
    {
        private Mock<IAsyncCommunicator> _asyncCommunicatorMock;
        private UserChangedPublisher _userChangedPublisher;
        private PublisherConfig _publisherConfig;

        [SetUp]
        public void SetUp()
        {
            _publisherConfig = new PublisherConfig
            {
                UserChangedQueue = "userChangedQueue"
            };

            _asyncCommunicatorMock = new Mock<IAsyncCommunicator>();
            _userChangedPublisher = new UserChangedPublisher(_publisherConfig, _asyncCommunicatorMock.Object);
        }

        [Test]
        public async Task Add_Should_Send()
        {
            var user = CreateTestUser();

            await _userChangedPublisher.Add(user);

            _asyncCommunicatorMock.Verify(a =>
                a.Send(_publisherConfig.UserChangedQueue, It.Is<string>(x => x.Contains("\"ChangeType\":0") && x.Contains("\"FirstName\":\"First Name\",\"LastName\":\" Last Name\",\"UserName\":\"username\""))),
                Times.Once);
        }

        [Test]
        public async Task Update_Should_Send()
        {
            var user = CreateTestUser();

            await _userChangedPublisher.Update(user);

            _asyncCommunicatorMock.Verify(a =>
                a.Send(_publisherConfig.UserChangedQueue, It.Is<string>(x => x.Contains("\"ChangeType\":1") && x.Contains("\"FirstName\":\"First Name\",\"LastName\":\" Last Name\",\"UserName\":\"username\""))),
                Times.Once);
        }

        [Test]
        public async Task Delete_Should_Send()
        {
            var user = CreateTestUser();

            await _userChangedPublisher.Delete(user.UserName);

            _asyncCommunicatorMock.Verify(a =>
                a.Send(_publisherConfig.UserChangedQueue, It.Is<string>(x => x.Contains("\"ChangeType\":2") && x.Contains("\"FirstName\":null,\"LastName\":null,\"UserName\":\"username\""))),
                Times.Once);
        }

        private User CreateTestUser() =>
            new User
            {
                FirstName = "First Name",
                LastName = " Last Name",
                Id = Guid.NewGuid(),
                UserName = "username"
            };
    }
}