using Services;
using System;
using System.ComponentModel.DataAnnotations;

namespace Main.DTO
{
    public class UserDto : IValidate
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
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
