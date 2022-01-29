using Model;
using System;
using System.Threading.Tasks;

namespace Publisher
{
    public interface IUserChangedPublisher
    {
        Task Add(User u);
        Task Update(User u);
        Task Delete(Guid u);
        Task Delete(string username);
    }
}
