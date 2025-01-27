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

     

        [HttpPost("upload-local")]
        public async Task<IActionResult> UploadImageToLocal([FromForm] ImageUploadDTO imageDto)
        {
            if (imageDto?.Image == null)
                return BadRequest("No image file provided.");

            var result = await _imageRepository.UploadImageToLocalAsync(imageDto);
            return Ok(result);
        }


        // [HttpPost("upload-cloudinary")]
        // public async Task<IActionResult> UploadImageToCloudinary([FromForm] ImageUploadDTO imageDto)
        // {
        //     if (imageDto?.Image == null)
        //         return BadRequest("No image file provided.");

        //     var result = await _imageRepository.UploadImageCloudinaryAsync(imageDto);
        //     return Ok(result);
        // }
    }
}
