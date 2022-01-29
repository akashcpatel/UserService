using System;

namespace Services
{
    public static class Guard
    {
        public static void Id(Guid id)
        {
            if (id == null || id == Guid.Empty)
                throw new ArgumentException($"{nameof(id)} is Null or Empty");
        }

        public static void FirstName(string firstName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException($"{nameof(firstName)} is Null or Empty");
        }

        public static void LastName(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException($"{nameof(lastName)} is Null or Empty");
        }

        public static void UserName(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentException($"{nameof(userName)} is Null or Empty");
        }
    }
}
