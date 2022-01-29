using Services;
using System;

namespace Main.DTO
{
    public class UserDto : IValidate
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public void Validate()
        {
            Guard.Id(Id);
            Guard.UserName(UserName);
            Guard.FirstName(FirstName);
            Guard.LastName(LastName);
        }

        public override string ToString()
        {
            return $"UserDto[Id = {Id}, UserName = {UserName}, FirstName = {FirstName}, LastName = {LastName}]";
        }
    }
}
