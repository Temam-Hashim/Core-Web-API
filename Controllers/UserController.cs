using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTO.User;
using WebAPI.Extensions;
using WebAPI.Mapper;
using WebAPI.Models;
using WebAPI.Repository;
using WebAPI.Service;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/users")]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _userRepository;
        public UserController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<UserResponseDTO>>> GetUsers()
        {
            var users = await _userRepository.GetUsersAsync();
            var userResponseDTOs = users.Select(user => user.ToUserResponse()).ToList();
            return Ok(userResponseDTOs);
        }

        [HttpGet("{userId}")]
        [Authorize]
        public async Task<IActionResult?> GetUserAsync([FromRoute] string userId)
        {
            var result = await _userRepository.GetUserAsync(userId);

            if (result is User user)
            {
                return Ok(user.ToUserResponse());
            }

            return result as ObjectResult;
        }


        [HttpPost]
        [Authorize]
        public async Task<ActionResult<CreateUserDTO>> CreateUser([FromBody] CreateUserDTO user)
        {

            var role = User.GetRole();
            if (role != "admin")
            {
                return Unauthorized("You are not authorized to create user.");
            }
            var createdUser = await _userRepository.CreateUserAsync(user);
            return Ok(createdUser);
        }


        [HttpPut("{userId}")]
        [Authorize]
        public async Task<IActionResult> UpdateUserAsync([FromBody] UpdateUserDTO userDto, string userId)
        {
            var tokenUserId = User.GetUserId();
            var role = User.GetRole();
            if (userId != tokenUserId || role != "admin")
            {
                return Unauthorized("You are not authorized to update this user.");
            }

            if (userDto == null) BadRequest("Invalid user data.");
            if (userId == null) BadRequest("Invalid user Id.");
            var result = await _userRepository.UpdateUserAsync(userDto, userId);
            return Ok(result.ToUserResponse());

        }



        [HttpDelete("{userId}")]
        [Authorize]
        public Task<object> DeleteUser([FromRoute] string userId)
        {
            var tokenUserId = User.GetUserId();
            var role = User.GetRole();
            if (userId != tokenUserId || role != "admin")
            {
                return (Task<object>)ApiResponseService.Error(409, "You are not authorized to delete this user.");
            }
            return _userRepository.DeleteUserAsync(userId);
        }
        [HttpPut("change-password/{userId}")]
        [Authorize]
        public async Task<IActionResult> ChangePasswordAsync([FromBody] ChangePasswordDTO changePasswordDto, string userId)
        {
            // Extracting user ID and role from the token
            var tokenUserId = User.GetUserId(); 
            var role = User.GetRole(); 

            // Debugging: Log or check the extracted user ID and role
            Console.WriteLine($"UserId from Token: {tokenUserId}, Role from Token: {role}");
            Console.WriteLine($"UserId from Request: {userId}");

            // Check if the user is authorized to change the password
            if (userId != tokenUserId && role != "admin")
            {
                return Unauthorized("You are not authorized to change password.");
            }

            try
            {
                if (changePasswordDto == null)
                    return ApiResponseService.Error(403, "Invalid response data");

                if (changePasswordDto.NewPassword != changePasswordDto.ConfirmPassword)
                    return ApiResponseService.Error(403, "New passwords do not match.");

                var result = await _userRepository.ChangePasswordAsync(userId, changePasswordDto.CurrentPassword, changePasswordDto.NewPassword);

                if (result)
                    return ApiResponseService.Success("Password changed successfully.", 200);
                else
                    return ApiResponseService.Error(403, "Current password is incorrect.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }

    }
}