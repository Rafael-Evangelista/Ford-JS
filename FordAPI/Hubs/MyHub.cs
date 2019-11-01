using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace FordAPI.Hubs
{
    [HubName("myHub")]
    public class MyHub : Hub
    {
        public void Subscribe(string lote)
        {
            Groups.Add(Context.ConnectionId, lote);
        }

        public void Unsubscribe(string lote)
        {
            Groups.Remove(Context.ConnectionId, lote);
        }
    }
}