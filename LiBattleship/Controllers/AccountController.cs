using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using LiBattleship.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace LiBattleship.Controllers
{
    [Produces("application/json")]
    [Route("api/Account")]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _config;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration config)
        {
            _userManager = userManager;
            _config = config;
            _signInManager = signInManager;
        }

        [Route("Register")]
        public async Task<IActionResult> Register(string userName, string password)
        {
            IdentityUser user = new IdentityUser(userName);
            var registerResult = await _userManager.CreateAsync(user, password);
            if (registerResult.Succeeded)
            {
                await _userManager.AddClaimAsync(user, Claims.BuildGuestClaim(false));
                var claimsPrincipal = await _signInManager.ClaimsFactory.CreateAsync(user);
                return Ok(BuildToken(claimsPrincipal.Identity as ClaimsIdentity));
            }
            return BadRequest(registerResult.Errors);
        }

        [Route("Login")]
        public async Task<IActionResult> Login(string userName, string password)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user != null)
            {
                if (await _userManager.CheckPasswordAsync(user, password))
                {
                    await _signInManager.SignInAsync(user, false);
                    var claimsPrincipal = await _signInManager.ClaimsFactory.CreateAsync(user);
                    return Ok(BuildToken(claimsPrincipal.Identity as ClaimsIdentity));
                }
            }
            return NotFound();
        }

        [Route("Guest")]
        public IActionResult GetGuestToket()
        {
            List<Claim> claims = new List<Claim>() { Claims.BuildGuestClaim(), Claims.BuildUserIdClaim() };
            var guest = new ClaimsIdentity(claims);
            return Ok(BuildToken(guest));
        }

        private string BuildToken(ClaimsIdentity claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var handler = new JwtSecurityTokenHandler();

            //var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Issuer"], claims, signingCredentials: creds);
            var token = handler.CreateJwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Issuer"], claims, signingCredentials: creds, expires: DateTime.Now.AddYears(1));
            return handler.WriteToken(token);
        }
    }
}