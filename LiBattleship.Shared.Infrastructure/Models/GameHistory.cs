using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LiBattleship.Shared.Infrastructure.Models
{
    [Table("GameHistories")]
    public class GameHistory
    {
        [Key]
        public Guid GameId { get; set; }

        public Guid WinnerId { get; set; }

        public IdentityUser<Guid> Winner { get; set; }

        public Guid LoserId { get; set; }

        public IdentityUser<Guid> Loser { get; set; }

        public DateTimeOffset StartTime { get; set; }

        public DateTimeOffset EndTime { get; set; }

        public string WinnerField { get; set; }

        public string LoserField { get; set; }
    }
}
