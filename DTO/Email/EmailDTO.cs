using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.DTO.Email
{
    public class EmailDTO
    {
        public required string From { get; set; } // Sender's email address
        public required string To { get; set; }   // Recipient's email address
        public required string Subject { get; set; }
        public required string Body { get; set; }
    }
}