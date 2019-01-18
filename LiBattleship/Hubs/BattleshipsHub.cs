using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiBattleship.Hubs
{
    [Authorize]
    public class BattleshipsHub : Hub
    {
        static ConcurrentBag<Guid> _users = new ConcurrentBag<Guid>();

        public override Task OnConnectedAsync()
        {
            _users.Add(Guid.Parse(Context.UserIdentifier));
            return base.OnConnectedAsync().ContinueWith((t) => this.Clients.All.SendAsync("setCount", _users.GroupBy(x => x).Count()));
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            Guid val = Guid.Parse(Context.UserIdentifier);
            _users.TryTake(out val);
            return base.OnDisconnectedAsync(exception).ContinueWith((t) => this.Clients.All.SendAsync("setCount", _users.GroupBy(x => x).Count()));
        }



    }
}
