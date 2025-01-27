using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.DTO.Image;

namespace WebAPI.Repository
{
    public class ImageRepository : IImageRepository
    {
        private readonly IConfiguration _configuration;
        // private readonly Cloudinary _cloudinary;
        private readonly string _localUploadFolder;

        public ImageRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _localUploadFolder = _configuration["FileStorage:LocalFolder"];

            // Cloudinary configuration
            // var cloudinarySettings = new CloudinaryDotNet.Account(
            //     _configuration["Cloudinary:CloudName"],
            //     _configuration["Cloudinary:ApiKey"],
            //     _configuration["Cloudinary:ApiSecret"]
            // );
            // _cloudinary = new Cloudinary(cloudinarySettings);

            // Local folder configuration
        }

    

        public async Task<ImageResponseDTO> UploadImageToLocalAsync(ImageUploadDTO imageDto)
        {
            // Upload to local folder
            var file = imageDto.Image;
            var filePath = Path.Combine(_localUploadFolder, file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return new ImageResponseDTO
            {
                Url = filePath,  // Returning local file path
                FileName = file.FileName
            };
        }


        // public Task<ImageResponseDTO> UploadImageToCloudinaryAsync(ImageUploadDTO imageDto)
        // {
        //     // Upload to Cloudinary
        //     var file = imageDto.Image;
        //     var uploadParams = new ImageUploadParams()
        //     {
        //         File = new FileDescription(file.FileName, file.OpenReadStream())
        //     };
        //     var uploadResult = await _cloudinary.UploadAsync(uploadParams);

        //     return new ImageResponseDTO
        //     {
        //         Url = uploadResult.Url.ToString(),
        //         FileName = uploadResult.PublicId
        //     };
        // }
    }
}