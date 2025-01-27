using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.DTO.Image;

namespace WebAPI.Repository
{
    public interface IImageRepository
    {
        // Task<ImageResponseDTO> UploadImageToCloudinaryAsync(ImageUploadDTO imageDto);
        Task<ImageResponseDTO> UploadImageToLocalAsync(ImageUploadDTO imageDto);
    }
}