using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiBattleship.Hubs
{
    public class BattleshipsHub : Hub
    {
        object _lock = new object();
        static int _count = 0;

        public override Task OnConnectedAsync()
        {
            lock (_lock)
            {
                _count++;
            }
            return base.OnConnectedAsync().ContinueWith((t) => this.Clients.All.SendAsync("setCount", _count));
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            lock (_lock)
            {
                _count--;
            }
            return base.OnDisconnectedAsync(exception).ContinueWith((t) => this.Clients.All.SendAsync("setCount", _count)); ;
        }



    }
}
