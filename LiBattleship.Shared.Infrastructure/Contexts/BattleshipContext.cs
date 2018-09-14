using LiBattleship.Shared.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LiBattleship.Shared.Infrastructure.Contexts
{
    public class BattleshipContext : IdentityDbContext<IdentityUser<Guid>, IdentityRole<Guid>, Guid>
    {
        public BattleshipContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<GameHistory> GameHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<GameHistory>()
               .HasOne(x => x.Winner)
               .WithMany()
               .HasForeignKey(x => x.WinnerId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<GameHistory>()
               .HasOne(x => x.Loser)
               .WithMany()
               .HasForeignKey(x => x.LoserId)
               .OnDelete(DeleteBehavior.Restrict);


            base.OnModelCreating(builder);
        }
    }
}
