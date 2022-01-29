using Microsoft.Extensions.Logging;
using Model;
using Moq;
using NUnit.Framework;
using Publisher;
using Services.Implementations;
using Storage;
using System;
using System.Threading.Tasks;

namespace Services.Tests.Implementations
{
    [TestFixture]
    public class UserServiceTests
    {
        private UserService _userService;
        private Mock<ILogger<UserService>> _loggerMock;
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IUserChangedPublisher> _userChangedPublisherMock;

        [SetUp]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger<UserService>>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _userChangedPublisherMock = new Mock<IUserChangedPublisher>();

            _userService = new UserService(_loggerMock.Object,
                _userChangedPublisherMock.Object, _userRepositoryMock.Object);
        }

        [Test]
        public async Task UpSert_Should_Add()
        {
            var testUser = CreateTestUser();

            _userRepositoryMock.Setup(r => r.Find(testUser.Id)).ReturnsAsync(() => null);

            await TestUpsert(1, testUser);
            _userChangedPublisherMock.Verify(p => p.Add(testUser), Times.Once);
        }

        [Test]
        public async Task UpSert_Should_Update()
        {
            var testUser = CreateTestUser();
            _userRepositoryMock.Setup(r => r.Find(testUser.Id)).ReturnsAsync(() => testUser);

            await TestUpsert(1, testUser);
            _userChangedPublisherMock.Verify(p => p.Update(testUser), Times.Once);
        }

        [Test]
        public async Task Delete_By_Id_Should_Call_Delete()
        {
            var testUser = CreateTestUser();

            _userRepositoryMock.Setup(r => r.Delete(testUser.Id));

            await _userService.Delete(testUser.Id);

            _userRepositoryMock.Verify(r => r.Delete(testUser.Id), Times.Once);
            _userChangedPublisherMock.Verify(p => p.Delete(testUser.Id), Times.Once);
        }

        [Test]
        public async Task Find_By_Id_Should_Call_Find()
        {
            var testUser = CreateTestUser();

            _userRepositoryMock.Setup(r => r.Find(testUser.Id)).ReturnsAsync(testUser);

            var user = await _userService.Find(testUser.Id);

            Assert.That(user.Id, Is.EqualTo(testUser.Id));
        }

        [Test]
        public async Task Find_By_UserName_Should_Call_Find()
        {
            var testUser = CreateTestUser();

            _userRepositoryMock.Setup(r => r.Find(testUser.UserName)).ReturnsAsync(testUser);

            var user = await _userService.Find(testUser.UserName);

            Assert.That(user.UserName, Is.EqualTo(testUser.UserName));
            Assert.That(user.Id, Is.EqualTo(testUser.Id));
        }

        private async Task TestUpsert(int saveCount, User testUser)
        {
            _userRepositoryMock.Setup(r => r.Save(testUser)).ReturnsAsync(testUser.Id);

            await _userService.UpSert(testUser);

            _userRepositoryMock.Verify(r => r.Find(testUser.Id), Times.Once);
            _userRepositoryMock.Verify(r => r.Save(testUser), Times.Exactly(saveCount));
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