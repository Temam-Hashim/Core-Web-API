using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTO.Account;
using WebAPI.DTO.Image;
using WebAPI.Interface;
using WebAPI.Repository;

namespace WebAPI.Controllers
{
    [Route("api/v1/auth")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;

        private readonly ImageRepository _imageRepository;

        public AccountController(IAccountRepository accountRepository, ImageRepository imageRepository)
        {
            _accountRepository = accountRepository;
            _imageRepository = imageRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] RegisterDTO registerDto)
        {
            // Handle image upload
            string profilePicture = null;

            // Handle image upload
            if (registerDto.ProfilePicture != null)
            {
                var imageUploadResult = await _imageRepository.UploadImageToLocalAsync(new ImageUploadDTO { Image = registerDto.ProfilePicture });
                profilePicture = imageUploadResult.Url; // Store the file path locally
            }

            var result = await _accountRepository.Register(registerDto, profilePicture);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            return await _accountRepository.Login(loginDto);
        }

    }
}
