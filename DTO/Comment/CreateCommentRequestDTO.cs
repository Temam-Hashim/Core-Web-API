using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.DTO.Comment
{
    public class CreateCommentRequestDTO
    {
        [Required]
        [MaxLength(100, ErrorMessage = "comment title cannot be more than 100 character")]
        public string? Title { get; set; }
        [Required]
        [MaxLength(180, ErrorMessage = "comment content cannot be more than 255 character")]
        [MinLength(10, ErrorMessage = "comment content cannot be less than 10 characters")]
        public string? Content { get; set; }
    }
}