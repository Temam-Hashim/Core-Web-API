using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    [Table("UserStock")]
    public class UserStock
    {

        public string UserId { get; set;}
        public Guid StockId { get; set;}

        // Navigation properties
        public Stock Stock { get; set;} 

        public User User { get; set; } // Assuming User has a UserId property
        
    }
}