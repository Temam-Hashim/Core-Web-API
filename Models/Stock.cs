using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    [Table("Stocks")]
    public class Stock
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        public string Symbol { get; set; } = "";
        public string CompanyName { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Purchase { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal LastDiv { get; set; }

        public string Industry { get; set; } = "";
        public long MarketCap { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public List<Comment> Comments { get; set; } = new List<Comment>();

        // Foreign key and navigation property
        [Required]
        public string UserId { get; set; } // Foreign key to User
        public User User { get; set; } // Navigation property
    }
}
