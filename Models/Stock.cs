using System.ComponentModel.DataAnnotations.Schema;


namespace WebAPI.Models
{
    public class Stock
    {
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

        // [JsonIgnore]
        // public  List<Comment> Comments { get; set; } = new List<Comment>();

        public List<Comment> Comments { get; set; } = [];


        // // // Foreign key for the User
        // public int UserId { get; set; }

        // // // Navigation property for the related user
        // public User User { get; set; }

    }


}