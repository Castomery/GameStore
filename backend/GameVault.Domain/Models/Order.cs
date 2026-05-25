using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameVault.Domain.Models
{
    public class Order
    {
        public int Id { get; set; } 
        public DateTime CreatedAt {  get; set; }
        public decimal TotalPrice { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
