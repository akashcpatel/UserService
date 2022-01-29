using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model;
using Storage.DataTables;
using System;
using System.Threading.Tasks;

namespace Storage.Implementations
{
    internal class UserRepository : IUserRepository
    {
        private readonly UserDataContext _context;

        public UserRepository([FromServices] UserDataContext context)
        {
            _context = context;
        }

        public async Task<Guid> Save(User u)
        {
            var userData = await _context.UserData.FindAsync(u.Id);

            if (userData == null)
                _context.UserData.Add(u.UserToUserData());
            else
            {
                userData.UserName = u.UserName;
                userData.FirstName = u.FirstName;
                userData.LastName = u.LastName;
            }

            _context.SaveChanges();

            return u.Id;
        }

        public async Task Delete(Guid id)
        {
            var userData = new UserData
            {
                Id = id
            };

            await Task.Run(() =>
            {
                _context.UserData.Attach(userData);
                _context.UserData.Remove(userData);
                _context.SaveChanges();
            });
        }

        public async Task<User> Find(Guid id)
        {
            var findUser = await _context.UserData.SingleOrDefaultAsync(u => u.Id == id);
            if (findUser != null)
                return findUser.UserDataToUser();
            return null;
        }

        public async Task<User> Find(string username)
        {
            var findUser = await _context.UserData.SingleOrDefaultAsync(u => u.UserName == username);
            if (findUser != null)
                return findUser.UserDataToUser();
            return null;
        }
    }
}
