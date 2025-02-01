using WebAPI.DTO.Image;
using WebAPI.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebAPI.Repository;

namespace WebAPI.Controllers
{
    [Route("api/v1/images")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageRepository _imageRepository;

        public ImageController(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

     

        [HttpPost("upload/local")]
        public async Task<IActionResult> UploadImageToLocal([FromForm] ImageUploadDTO imageDto)
        {
            if (imageDto?.File == null)
                return BadRequest("No image file provided.");

            var result = await _imageRepository.UploadImageToLocalAsync(imageDto);
            return Ok(result);
        }


        [HttpPost("upload/cloudinary")]
        public async Task<object> UploadImageToCloudinary([FromForm] ImageUploadDTO imageDto)
        {
            // return  await _imageRepository.UploadImageToCloudinaryAsync(file);
            {
                if (imageDto.File == null)
                {
                    return BadRequest("No file uploaded.");
                }

                try
                {
                    var imageUrl = await _imageRepository.UploadImageToCloudinaryAsync(imageDto.File);
                    return Ok(new { ImageUrl = imageUrl });
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }
        }
    }
}
