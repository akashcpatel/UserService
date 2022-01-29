using System.Threading.Tasks;

namespace Publisher
{
    internal interface IAsyncCommunicator
    {
        Task Send(string queueName, string message);

        Task<T> Receive<T>(string queueName);
    }
}
