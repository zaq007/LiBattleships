using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using LiBattleship.Command;
using LiBattleship.Command.Commands.Game;
using Microsoft.AspNetCore.Mvc;

namespace LiBattleship.Controllers
{
    public class HomeController : Controller
    {
        ICommandHandler<GameResultCommand> gameResultHandler;

        public HomeController(ICommandHandler<GameResultCommand> gameResultHandler)
        {
            this.gameResultHandler = gameResultHandler;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }

        public IActionResult Test()
        {
            gameResultHandler.Handle(new GameResultCommand());
            return Ok();
        }
    }
}
