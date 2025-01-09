using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Data;
using WebAPI.DTO.Account;
using WebAPI.Models;

namespace WebAPI.Controllers
{

    [Route("api/v1/auth")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDBContext _dBContext;

        public AccountController(UserManager<User> userManager, ApplicationDBContext dBContext)
        {
            _userManager = userManager;
            _dBContext = dBContext;
        }

        [HttpPost("/register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest();

                var user = new User
                {
                    UserName = registerDto.UserName,
                    Email = registerDto.Email,
                    FirstName = registerDto.FirstName,
                    LastName = registerDto.LastName,
                    PhoneNumber = registerDto.PhoneNumber
                };

                var createdUser = await _userManager.CreateAsync(user, registerDto.Password);

                if (createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(user, "user");
                    if (roleResult.Succeeded)
                    {
                        var result = new
                        {
                            user.Id,
                            user.UserName,
                            user.FirstName,
                            user.LastName,
                            user.Email,
                            user.PhoneNumber
                        };

                        return Ok(result);
                    }
                    else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }

                }else{
                    return StatusCode(500, createdUser.Errors);
                }

            }
            catch (Exception e)
            {

                return StatusCode(500, e);
            }

          
        }
    }
}