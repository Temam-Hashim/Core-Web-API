using System.ComponentModel.DataAnnotations;



namespace WebAPI.DTO
{
    public class StockDTO
    {
        public Guid Id { get; set; }
        [Required]
        [MaxLength(180, ErrorMessage = "Symbol cannot be more than 180 character")]
        public string? Symbol { get; set; }

        [Required]
        [MaxLength(180, ErrorMessage = "Company Name cannot be more than 180 character")]
        public string? CompanyName { get; set; }

        [Required]
        [Range(0, 999999999999)]
        public decimal Purchase { get; set; }

        [Required]
        [Range(0, 9)]
        public decimal LastDiv { get; set; }

        [Required]
        [MaxLength(180, ErrorMessage = "Industry cannot be more than 180 character")]
        // [MinLength(5, ErrorMessage = "Industry must be at least 5 characters")]
        public string? Industry { get; set; }

        [Required]
        [Range(0, 999999999999)]
        public long MarketCap { get; set; }

        public string CreatedBy { get; set; } = string.Empty;

        public List<CommentDTO>? Comments { get; set; }
    }
}