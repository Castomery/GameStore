using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameVault.Application.Dtos.Order
{
    public class OrderItemDto
    {
        public int GameId { get; set; }
        public string GameTitle { get; set; } = string.Empty;
        public string CoverImageUrl { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
