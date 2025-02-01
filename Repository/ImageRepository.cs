using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using WebAPI.DTO.Image;

namespace WebAPI.Repository
{
    public class ImageRepository : IImageRepository
    {
        private readonly IConfiguration _config;
        private readonly Cloudinary _cloudinary;
        private readonly string _localUploadFolder;

        public ImageRepository(IConfiguration config)
        {
            _config = config;
            _localUploadFolder = _config["FileStorage:LocalFolder"];

            var account = new Account(
            _config["Cloudinary:CloudName"],
            _config["Cloudinary:ApiKey"],
            _config["Cloudinary:ApiSecret"]
        );

            _cloudinary = new Cloudinary(account);
        }



        public async Task<ImageResponseDTO> UploadImageToLocalAsync(ImageUploadDTO imageDto)
        {
            // Upload to local folder
            var file = imageDto.File;
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


        public async Task<object> UploadImageToCloudinaryAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("File is empty or null.");
            }

            // Convert the file to a byte array
            using var stream = new MemoryStream();
            await file.CopyToAsync(stream);
            var fileBytes = stream.ToArray();

            // Upload the image to Cloudinary
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, new MemoryStream(fileBytes)),
                PublicId = Guid.NewGuid().ToString() // Generate a unique public ID
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            // Return the secure URL of the uploaded image
            return new
            {
                message = "Image uploaded successfully",
                secureUrl = uploadResult.SecureUrl.ToString(),
                statusCode = 200,
            };
        }

   
    }

}