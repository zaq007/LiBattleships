using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LiBattleship.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        [Route("")]
        [HttpPost]
        public IActionResult Create([FromBody] int[][] field)
        {
            return Ok(field);
        }
    }
}