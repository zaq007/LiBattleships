using LiBattleship.Command.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace LiBattleship.Command.Models
{
    public class CommandResult
    {
        public CommandResult(bool result, string message)
        {
            if (result == true)
            {
                Result = CommandResultType.Ok;
            }
            else
            {
                Result = CommandResultType.Error;
                Message = message;
            }
        }

        public CommandResultType Result { get; set;  }

        public string Message { get; set; }
    }
}
