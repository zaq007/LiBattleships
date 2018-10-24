using LiBattleship.Command.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LiBattleship.Command
{
    public interface ICommandHandler<T> where T: ICommand
    {
        CommandResult Handle(T command);
    }
}
