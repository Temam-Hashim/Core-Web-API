using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.DTO
{
    public class CommentDTO
    {
        public Guid Id { get; set; }
        // [Required]
        // [MaxLength(100, ErrorMessage="comment title cannot be more than 100 character")]
        // [MinLength(10, ErrorMessage="comment title cannot be less than 10 characters")]
        public string? Title { get; set; }
        // [Required]
        // [MaxLength(100, ErrorMessage = "comment content cannot be more than 255 character")]
        // [MinLength(10, ErrorMessage = "comment content cannot be less than 10 characters")]
        public string? Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid? StockId { get; set; }
    }
}