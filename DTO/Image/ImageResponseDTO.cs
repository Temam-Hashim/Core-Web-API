using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.DTO.Image
{
    public class ImageResponseDTO
    {
        public required string Url { get; set; }
        public required string FileName { get; set; }
    }
}