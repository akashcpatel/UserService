using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Model;
using Storage.DataTables;
using System;
using System.Threading.Tasks;

namespace Storage.Implementations
{
    internal class UserRepository : IUserRepository
    {
        private readonly UserDataContext _context;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository([FromServices] UserDataContext context, ILogger<UserRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Guid> Save(User u)
        {
            var userData = await _context.UserData.FindAsync(u.Id);

            try
            {
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
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while saving user for {user}.", u, ex);

                if(ex.InnerException != null)
                    throw new Exception(ex.InnerException.Message);
                throw new Exception(ex.Message);
            }
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
