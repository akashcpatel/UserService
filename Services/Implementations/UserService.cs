using Microsoft.Extensions.Logging;
using Model;
using Publisher;
using Storage;
using System;
using System.Threading.Tasks;

namespace Services.Implementations
{
    internal class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IUserChangedPublisher _publisher;

        public UserService(ILogger<UserService> logger,
            IUserChangedPublisher publisher, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
            _publisher = publisher;
        }

        public async Task<Guid?> UpSert(User u)
        {
            _logger.LogInformation("UpSert user = {user}", u);
            var user = await _userRepository.Find(u.Id);
            if (user == null)
                await Add(u);
            else
                await Update(u);

            _logger.LogInformation("UpSert user completed for {user}", u);
            return await Task.FromResult(u?.Id);
        }

        public async Task<bool> Delete(Guid id)
        {
            _logger.LogInformation("Delete user {id}", id);

            await _userRepository.Delete(id);
            _ = Task.Run(async () => await _publisher.Delete(id));

            _logger.LogInformation("Delete user completed for id = {id}", id);
            return await Task.FromResult(true);
        }

        public async Task<User> Find(Guid id)
        {
            _logger.LogInformation("Find user for id = {id}.", id);

            var user = await _userRepository.Find(id);

            if (user == null)
                _logger.LogInformation("Did not find user for id = {id}.", id);
            else
                _logger.LogInformation("Found user {user} for id = {id}.", user, id);

            return user;
        }

        public async Task<User> Find(string username)
        {
            _logger.LogInformation("Find user for username {username}", username);

            var user = await _userRepository.Find(username);
            if (user == null)
                _logger.LogInformation("Did not find user for username = {username}.", username);
            else
                _logger.LogInformation("Found user {user} for username = {username}.", user, username);

            return user;
        }

        private async Task Add(User u)
        {
            _logger.LogInformation("Add user {user}", u);

            await _userRepository.Save(u);
            _ = Task.Run(async () => await _publisher.Add(u));
        }

        private async Task Update(User u)
        {
            _logger.LogInformation("Update user {user}", u);

            await _userRepository.Save(u);
            _ = Task.Run(async () => await _publisher.Update(u));
        }
    }
}
