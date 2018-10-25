using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace RedHatForumSpain2018.Infrastructure
{
    public class TemperatureHub : Hub
    {
        public async Task SendMessage(object message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}