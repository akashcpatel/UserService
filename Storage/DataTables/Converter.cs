using Model;

namespace Storage.DataTables
{
    internal static class ConversionExtensions
    {
        public static UserData UserToUserData(this User u) =>
            new UserData
            {
                Id = u.Id,
                UserName = u.UserName,
                FirstName = u.FirstName,
                LastName = u.LastName
            };

        public static User UserDataToUser(this UserData u) =>
            new User
            {
                Id = u.Id,
                UserName = u.UserName,
                FirstName = u.FirstName,
                LastName = u.LastName
            };
    }
}