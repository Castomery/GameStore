using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameVault.Domain.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public float Rating { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
        public int GameId { get; set; }
        public Game Game { get; set; }
        public User User { get; set; }

    }
}
