using Main.Controllers;
using Main.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Model;
using Moq;
using NUnit.Framework;
using Services;
using System;
using System.Threading.Tasks;

namespace Main.Tests.Controllers
{
    [TestFixture]
    internal class UserControllerTests
    {
        private Mock<IUserService> _userServiceMock;
        private UserController _controller;

        [SetUp]
        public void SetUp()
        {
            var _loggerMock = new Mock<ILogger<UserController>>();
            _userServiceMock = new Mock<IUserService>();

            _controller = new UserController(_loggerMock.Object, _userServiceMock.Object);
        }

        [Test]
        public async Task Save_Should_Throw_ValidationError()
        {
            var dto = new UserDto();
            string exceptionMessage = "";
            try
            {
                await _controller.Save(dto);
            }
            catch (ArgumentException ex)
            {
                exceptionMessage = ex.Message;
            }

            Assert.That(exceptionMessage, Is.EqualTo("id is Null or Empty"));

            _userServiceMock.Verify(u => u.UpSert(It.IsAny<User>()), Times.Never);
        }

        [Test]
        public async Task Save_Should_UpSert()
        {
            var dto = CreateTestUser();

            ActionResult<Guid> upsertResponse = null;

            _userServiceMock.Setup(
                u =>
                u.UpSert(It.Is<User>(u => u.FirstName == dto.FirstName && u.LastName == dto.LastName &&
                                          u.UserName == dto.UserName && u.Id == dto.Id))).ReturnsAsync(dto.Id);

            upsertResponse = await _controller.Save(dto);

            _userServiceMock.Verify(u => u.UpSert(It.Is<User>(u => 
                            u.FirstName == dto.FirstName && u.LastName == dto.LastName && 
                            u.UserName == dto.UserName && u.Id == dto.Id)), Times.Once);

            Assert.That(((ObjectResult)upsertResponse.Result).Value, Is.EqualTo(dto.Id));
        }

        [Test]
        public async Task Get_By_Id_Should_Throw_ValidationError()
        {
            string exceptionMessage = "";
            try
            {
                await _controller.Get(Guid.Empty);
            }
            catch (ArgumentException ex)
            {
                exceptionMessage = ex.Message;
            }

            _userServiceMock.Verify(u => u.Find(It.IsAny<string>()), Times.Never());
            Assert.That(exceptionMessage, Is.EqualTo("id is Null or Empty"));
        }

        [Test]
        public async Task Get_By_Id_Should_Find()
        {
            var dto = CreateTestUser();

            var user = dto.ToModel();

            ActionResult<Guid> findResponse = null;

            _userServiceMock.Setup(u => u.Find(dto.Id)).ReturnsAsync(user);

            findResponse = await _controller.Get(dto.Id);
            var foundUser = (User)((ObjectResult)findResponse.Result).Value;

            Assert.True(foundUser.Equals(user));
        }

        [Test]
        public async Task Get_By_UserName_Should_Throw_ValidationError()
        {
            string exceptionMessage = "";
            try
            {
                await _controller.Get("");
            }
            catch (ArgumentException ex)
            {
                exceptionMessage = ex.Message;
            }

            _userServiceMock.Verify(u => u.Find(It.IsAny<string>()), Times.Never());
            Assert.That(exceptionMessage, Is.EqualTo("userName is Null or Empty"));
        }

        [Test]
        public async Task Get_By_UserName_Should_Find()
        {
            var dto = CreateTestUser();

            var user = dto.ToModel();

            ActionResult<Guid> findResponse = null;

            _userServiceMock.Setup(u => u.Find(dto.UserName)).ReturnsAsync(user);

            findResponse = await _controller.Get(dto.UserName);
            var foundUser = (User)((ObjectResult)findResponse.Result).Value;

            Assert.True(foundUser.Equals(user));
        }

        [Test]
        public async Task Delete_By_Id_Should_Throw_ValidationError()
        {
            string exceptionMessage = "";
            try
            {
                await _controller.Delete(Guid.Empty);
            }
            catch (ArgumentException ex)
            {
                exceptionMessage = ex.Message;
            }

            _userServiceMock.Verify(u => u.Find(It.IsAny<string>()), Times.Never());
            Assert.That(exceptionMessage, Is.EqualTo("id is Null or Empty"));
        }

        public async Task Delete_By_Id_When_Delete_Pass()
        {
            var dto = CreateTestUser();

            var user = dto.ToModel();

            ActionResult<Guid> findResponse = null;

            _userServiceMock.Setup(u => u.Delete(dto.Id)).ReturnsAsync(true);

            findResponse = await _controller.Delete(dto.Id);
            var deleteResult = ((StatusCodeResult)findResponse.Result).StatusCode;

            Assert.That(deleteResult, Is.EqualTo(200));
        }

        [Test]
        public async Task Delete_By_Id_When_Delete_Fail()
        {
            var dto = CreateTestUser();

            var user = dto.ToModel();

            ActionResult<Guid> findResponse = null;

            _userServiceMock.Setup(u => u.Delete(dto.Id)).ReturnsAsync(false);

            findResponse = await _controller.Delete(dto.Id);
            var deleteResultMessage = ((ObjectResult)findResponse.Result).Value.ToString();

            Assert.That(((ObjectResult)findResponse.Result).StatusCode, Is.EqualTo(417));
            Assert.True(deleteResultMessage.Contains("Failed to delete User for Id"));
        }

        private UserDto CreateTestUser() =>
            new UserDto
            {
                FirstName = "First Name",
                LastName = " Last Name",
                Id = Guid.NewGuid(),
                UserName = "username"
            };
    }
}
