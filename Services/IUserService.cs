using Model;
using System;
using System.Threading.Tasks;

namespace Services
{
    public interface IUserService
    {
        Task<Guid?> UpSert(User u);
        Task<bool> Delete(Guid id);
        Task<User> Find(Guid id);
        Task<User> Find(string username);
    }
}
