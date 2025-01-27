using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.DTO.User
{
    public class ChangePasswordDTO
    {

        public required string CurrentPassword { get; set; }
        public required string NewPassword { get; set; }
        // [Required]
        // [Compare("NewPassword", ErrorMessage = "Passwords do not match")]
        public required string ConfirmPassword { get; set; }
   
    }
}