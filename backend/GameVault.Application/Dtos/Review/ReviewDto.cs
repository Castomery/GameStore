using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameVault.Application.Dtos.Review
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public float Rating { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UserName { get; set; } = string.Empty;
    }
}
