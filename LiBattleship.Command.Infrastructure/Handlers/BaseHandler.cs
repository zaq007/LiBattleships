using LiBattleship.Shared.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace LiBattleship.Command.Infrastructure.Handlers
{
    class BaseHandler
    {
        protected BattleshipContext context;

        public BaseHandler(BattleshipContext context)
        {
            this.context = context;
        }
    }
}
