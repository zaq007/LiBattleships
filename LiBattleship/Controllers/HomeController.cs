using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using LiBattleship.Command;
using LiBattleship.Command.Commands.Game;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LiBattleship.Controllers
{
    public class HomeController : Controller
    {
        IMediator mediator;

        public HomeController(IMediator mediator)
        {
            this.mediator = mediator;
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
            mediator.Send(new GameResultCommand());
            return Ok();
        }
    }
}
