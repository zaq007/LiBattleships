using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiBattleship.Command.Infrastructure.Extensions;
using LiBattleship.Game;
using LiBattleship.Game.Infrastructure;
using LiBattleship.Hubs;
using LiBattleship.Identity;
using LiBattleship.Matchmaking;
using LiBattleship.Matchmaking.Infrastructure;
using LiBattleship.Service.Infrastructure.Services;
using LiBattleship.Service.Services;
using LiBattleship.Services;
using LiBattleship.Shared.Infrastructure.Contexts;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

namespace LiBattleship
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BattleshipContext>(options => {
                //options.UseInMemoryDatabase("BattleShipsDB");
                options.UseSqlServer(Configuration.GetConnectionString("BattleshipsDb"));
            });
            services.AddIdentity<IdentityUser<Guid>, IdentityRole<Guid>>((x =>
            {
            })).AddEntityFrameworkStores<BattleshipContext>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
        .AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = Configuration["Jwt:Issuer"],
                ValidAudience = Configuration["Jwt:Issuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])),
            };
            options.Events = new JwtBearerEvents();
            options.Events.OnMessageReceived = (context) =>
            {
                if (context.HttpContext.WebSockets.IsWebSocketRequest && context.Request.Query.ContainsKey("access_token"))
                {
                    context.Token = context.Request.Query["access_token"];
                }

                return Task.CompletedTask;
            };
        });
            services.AddSignalR(x => x.EnableDetailedErrors = true);
            services.AddMvc();

            services.AddSingleton<IMatchmaking, Matchmaking.Infrastructure.Matchmaking>();
            services.AddSingleton<IGameServer, GameServer>();
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<IGameService, GameService>();

            services.AddCommandHandlers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true,
                    ReactHotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseWebSockets();
            app.UseAuthentication();
            app.UseSignalR((x) =>
            {
                x.MapHub<BattleshipsHub>("/hubs/battleships");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}
