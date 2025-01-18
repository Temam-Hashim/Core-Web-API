
// using System.IdentityModel.Tokens.Jwt;
// using System.Security.Claims;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.AspNetCore.Mvc;
// using WebAPI.Data;
// using WebAPI.Extensions;
// using WebAPI.Interface;
// using WebAPI.Models;
// using WebAPI.Service;

// namespace WebAPI.Controllers
// {
//     [Route("api/userstock")]
//     [ApiController]
//     public class UserStockController: ControllerBase
//     {
//         private readonly UserManager<User> _userManager;
//         private readonly IStockRepository _stockRepository;

//         private readonly ApplicationDBContext _context;
//         private readonly IUserStockRepository _userStockRepository;

//         public UserStockController(UserManager<User> userManager, IStockRepository stockRepository, ApplicationDBContext context, IUserStockRepository userStockRepository){
//             _userManager = userManager;
//             _stockRepository = stockRepository;
//             _context = context;
//             _userStockRepository = userStockRepository;
//         }

//         [HttpGet]
//         [Authorize]
//         public async Task<IActionResult> GetUserStock(){
//             // var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
//             // Extract the email from the token claims
//             // var emailClaim = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Email)?.Value;

//             var email  = User.GetEmail();
//             var user = await _userManager.FindByEmailAsync(email);
//             var userStock = await _userStockRepository.GetUserStock(user);

//             return Ok(userStock);



//         }

//     }
// }