using Model;
using Newtonsoft.Json;
using Publisher.Message;
using System;
using System.Threading.Tasks;

namespace Publisher.Implementations
{
    internal class UserChangedPublisher : IUserChangedPublisher
    {
        private readonly PublisherConfig _config;
        private readonly IAsyncCommunicator _asyncCommunicator;

        public UserChangedPublisher(PublisherConfig config, IAsyncCommunicator asyncCommunicator)
        {
            _config = config;
            _asyncCommunicator = asyncCommunicator;
        }

        public async Task Add(User u)
        {
            await _asyncCommunicator.Send(_config.UserChangedQueue, CreateMessage(u, ChangeType.Add));
        }

        public async Task Delete(Guid id)
        {
            await _asyncCommunicator.Send(_config.UserChangedQueue, CreateMessage(new User { Id = id }, ChangeType.Delete));
        }

        public async Task Delete(string username)
        {
            await _asyncCommunicator.Send(_config.UserChangedQueue, CreateMessage(new User { UserName = username }, ChangeType.Delete));
        }

        public async Task Update(User u)
        {
            await _asyncCommunicator.Send(_config.UserChangedQueue, CreateMessage(u, ChangeType.Update));
        }

        private string CreateMessage(User u, ChangeType changeType)
        {
            var data = new Data
            {
                Header = CreateHeader(changeType),
                Payload = u
            };

            var message = JsonConvert.SerializeObject(data);
            return message;
        }

        private Header CreateHeader(ChangeType changeType) =>
            new Header
            {
                Key = Guid.NewGuid(),
                ChangeType = changeType
            };
    }
}
