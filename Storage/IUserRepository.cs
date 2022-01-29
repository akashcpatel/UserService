using Model;
using System;
using System.Threading.Tasks;

namespace Storage
{
    public interface IUserRepository
    {
        Task<Guid> Save(User u);
        Task Delete(Guid id);
        Task<User> Find(Guid id);
        Task<User> Find(string username);
    }
}
