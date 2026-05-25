using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameVault.Domain.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int GameId { get; set; }
        public Game Game { get; set; }
    }
}
