using LiBattleship.Hubs;
using LiBattleship.Shared.Models;
using LiBattleship.Shared.Services;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiBattleship.Services
{
    public class NotificationService : INotificationService
    {
        const string SET_GAME_LIST_ACTION = "setGameList";
        const string GAME_CREATED_ACTION = "gameCreated";
        const string SET_GAME_STATE_ACTION = "setGameState";

        private readonly IHubContext<BattleshipsHub> hub;

        public NotificationService(IHubContext<BattleshipsHub> hub)
        {
            this.hub = hub;
        }

        public void GameCreated(Guid userId, PlayerGameState state)
        {
            User(userId).SendAsync(GAME_CREATED_ACTION, state);
        }

        public void SetGameList(IEnumerable<Match> matches)
        {
            hub.Clients.All.SendAsync(SET_GAME_LIST_ACTION, matches);
        }

        public void SetGameState(Guid userId, PlayerGameState state)
        {
            User(userId).SendAsync(SET_GAME_STATE_ACTION, state);
        }

        private IClientProxy User(Guid userId)
        {
            return hub.Clients.User(userId.ToString());
        }
    }
}
