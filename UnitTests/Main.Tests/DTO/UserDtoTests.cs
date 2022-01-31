using Main.DTO;
using NUnit.Framework;
using System;

namespace Main.Tests.DTO
{
    public class UserDtoTests
    {
        [Test]
        public void Test_Validate()
        {
            var dto = new UserDto();
            Test_UserDto_Validation(dto, "id is Null or Empty");

            dto.Id = Guid.NewGuid();
            Test_UserDto_Validation(dto, "userName is Null or Empty");

            dto.UserName = "username";
            Test_UserDto_Validation(dto, "firstName is Null or Empty");

            dto.FirstName = "First Name";
            Test_UserDto_Validation(dto, "lastName is Null or Empty");

            dto.LastName = "Last Name";
            Test_UserDto_Validation(dto, string.Empty);
        }

        public void Test_UserDto_Validation(UserDto dto, string expectedExceptionMessage)
        {
            var exceptionMessage = string.Empty;
            try
            {
                dto.Validate();
            }
            catch (Exception ex)
            {
                exceptionMessage = ex.Message;
            }

            Assert.That(exceptionMessage, Is.EqualTo(expectedExceptionMessage));
        }
    }
}
