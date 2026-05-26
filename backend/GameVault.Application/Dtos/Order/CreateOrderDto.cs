using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameVault.Application.Dtos.Order
{
    public class CreateOrderDto
    {
        [Required]
        public List<int> GameIds { get; set; } = new();
    }
}
