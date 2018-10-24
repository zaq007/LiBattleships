using LiBattleship.Command.Commands.Game;
using LiBattleship.Command.Infrastructure.Handlers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace LiBattleship.Command.Infrastructure.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddCommandHandlers(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<ICommandHandler<GameResultCommand>, GameHandler>();
        }
    }
}
