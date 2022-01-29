using Model;

namespace Main.DTO
{
    public static class DtoExtensions
    {
        public static User ToModel(this UserDto dto) =>
            new User
            {
                UserName = dto.UserName,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Id = dto.Id
            };

        public static UserDto ToDto(this User u) =>
            new UserDto
            {
                UserName = u.UserName,
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName
            };
    }
}
