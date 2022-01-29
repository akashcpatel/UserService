using Main.DTO;
using Model;
using NUnit.Framework;
using System;

namespace Main.Tests.DTO
{
    public class DtoExtensionsTests
    {
        [Test]
        public void Test_ToModel()
        {
            var dto = new UserDto
            {
                FirstName = "firstName",
                LastName = "lastName",
                Id = Guid.NewGuid(),
                UserName = "username"
            };

            var model = dto.ToModel();

            Assert.That(dto.Id, Is.EqualTo(model.Id));
            Assert.That(dto.FirstName, Is.EqualTo(model.FirstName));
            Assert.That(dto.LastName, Is.EqualTo(model.LastName));
            Assert.That(dto.UserName, Is.EqualTo(model.UserName));
        }

        [Test]
        public void Test_ToDto()
        {
            var model = new User
            {
                FirstName = "firstName",
                LastName = "lastName",
                Id = Guid.NewGuid(),
                UserName = "username"
            };

            var dto = model.ToDto();

            Assert.That(dto.Id, Is.EqualTo(model.Id));
            Assert.That(dto.FirstName, Is.EqualTo(model.FirstName));
            Assert.That(dto.LastName, Is.EqualTo(model.LastName));
            Assert.That(dto.UserName, Is.EqualTo(model.UserName));
        }
    }
}
