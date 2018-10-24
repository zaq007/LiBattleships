using LiBattleship.Command.Commands.Game;
using LiBattleship.Command.Models;
using LiBattleship.Shared.Infrastructure.Contexts;
using LiBattleship.Shared.Infrastructure.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LiBattleship.Command.Infrastructure.Handlers
{
    class GameHandler : BaseHandler, ICommandHandler<GameResultCommand>
    {


        public GameHandler(BattleshipContext context) : base(context)
        {

        }

        public CommandResult Handle(GameResultCommand command)
        {
            context.GameHistories.Add(new GameHistory
            {
                GameId = command.GameId,
                Player1Id = command.Player1,
                EndTime = command.EndTime,
                Player1Field = JsonConvert.SerializeObject(command.Player1Field),
                Player2Field = JsonConvert.SerializeObject(command.Player2Field),
                Player2Id = command.Player2,
                StartTime = command.StartTime,
                WinnerId = command.Winner
            });
            return new CommandResult(context.SaveChanges() > 0, null);
        }
    }
}
