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

        public Guid Player1Id { get; set; }

        public IdentityUser<Guid> Player1 { get; set; }

        public Guid Player2Id { get; set; }

        public IdentityUser<Guid> Player2 { get; set; }

        public DateTimeOffset StartTime { get; set; }

        public DateTimeOffset EndTime { get; set; }

        public string Player1Field { get; set; }

        public string Player2Field { get; set; }
    }
}
