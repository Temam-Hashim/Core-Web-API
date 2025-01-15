using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTO.Account;
using WebAPI.Interface;

namespace WebAPI.Controllers
{
    [Route("api/v1/auth")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDto)
        {
            return await _accountRepository.Register(registerDto);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            return await _accountRepository.Login(loginDto);
        }

    }
}
