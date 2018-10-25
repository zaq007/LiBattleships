using LiBattleship.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LiBattleship.Shared.Services
{
    public interface INotificationService
    {
        void SetGameList(IEnumerable<Match> matches);

        void GameCreated(Guid userId, PlayerGameState state);

        void SetGameState(Guid userId, PlayerGameState state);
    }
}
