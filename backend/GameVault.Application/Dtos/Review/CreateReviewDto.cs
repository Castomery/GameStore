using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameVault.Application.Dtos.Review
{
    public class CreateReviewDto
    {
        [Required]
        public string Text { get; set; } = string.Empty;
        [Required]
        [Range(1,10)]
        public float Rating { get; set; }
    }
}
