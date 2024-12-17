

using System.Text.Json.Serialization;

namespace WebAPI.Models
{
    public class Comment
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = "";

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public Guid? StockId { get; set; } // Change to Guid? to match Stock's primary key type
        
        [JsonIgnore]
        public Stock? Stock { get; set; } // Navigation property

    
    }
}