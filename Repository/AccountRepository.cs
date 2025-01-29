using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Data;
using WebAPI.DTO.Account;
using WebAPI.Interface;
using WebAPI.Mapper;
using WebAPI.Models;
using WebAPI.Service;

namespace WebAPI.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ApplicationDBContext _dBContext;

        private readonly ITokenService _tokenService;

        public AccountRepository(UserManager<User> userManager, ApplicationDBContext dBContext, ITokenService tokenService, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _dBContext = dBContext;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Register(RegisterDTO registerDto, string profilePicture)
        {
            try
            {
                if (registerDto == null) return new BadRequestResult();

                var user = registerDto.ToRegisterDTO();


                // Set the profile image path if available
                if (!string.IsNullOrEmpty(profilePicture))
                {
                    user.ProfilePicture = profilePicture;
                }

                var createdUser = await _userManager.CreateAsync(user, registerDto.Password);
                if (createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(user, "user");
                    if (roleResult.Succeeded)
                    {
                        var result = new RegisterResponseDTO
                        {
                            UserName = user.UserName,
                            Email = user.Email,
                            Token = _tokenService.CreateToken(user).Result,
                        };


                        return new OkObjectResult(result);
                    }
                    else
                    {
                        var failedResponse = ApiResponseService.Error(500, "operation failed", roleResult.Errors);
                        return new ObjectResult(failedResponse);
                    }
                }
                else
                {
                    // return new ObjectResult(createdUser.Errors)
                    var failedResponse = ApiResponseService.Error(500, "operation failed", createdUser.Errors);
                    return new ObjectResult(failedResponse);
                }
            }
            catch (Exception e)
            {
                return new ObjectResult(e.Message) { StatusCode = 500 };
            }
        }




        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);
            if (user == null) {
                var noUserResponse = ApiResponseService.Error(401, "No user found with email " + loginDTO.Email);
                return new UnauthorizedObjectResult(noUserResponse);
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDTO.Password, false);
            if (!result.Succeeded) {
                var invalidResponse = ApiResponseService.Error(401, "Invalid username or password.");
                return new UnauthorizedObjectResult(invalidResponse);
            }

            var token = await _tokenService.CreateToken(user);

            var response = new LoginResponseDTO
            {
                Id = user.Id,
                Email = user.Email,
                Token = token,
            };

            // return new OkObjectResult(successResponse);
            var successResponse = ApiResponseService.Success("Login successful", response);
            return new OkObjectResult(successResponse);
        }
    }
}

