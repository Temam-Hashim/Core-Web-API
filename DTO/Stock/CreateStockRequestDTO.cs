using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.DTO
{
    public class CreateStockRequestDTO
    {
        [Required]
        [MaxLength(180, ErrorMessage = "Symbol cannot be more than 180 character")]
        public required string  Symbol { get; set; }

        [Required]
        [MaxLength(180, ErrorMessage = "Company Name cannot be more than 180 character")]
        public required string CompanyName { get; set; }
        
        [Required]
        [Range(0,999999999999)]
        public required decimal Purchase { get; set; }

        [Required]
        [Range(0, 9)]
        public required decimal LastDiv { get; set; }

        [Required]
        [MaxLength(180, ErrorMessage = "Industry cannot be more than 180 character")]
        // [MinLength(5, ErrorMessage = "Industry must be at least 5 characters")]
        public required string Industry { get; set; }

        [Required]
        [Range(0, 999999999999)]
        public required long MarketCap { get; set; }
    }
}