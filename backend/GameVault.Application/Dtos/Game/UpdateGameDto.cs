using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameVault.Application.Dtos.Game
{
    public class UpdateGameDto
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [Required]
        public decimal Price { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string CoverImageUrl { get; set; } = string.Empty;
        public int GenreId { get; set; }
    }
}
