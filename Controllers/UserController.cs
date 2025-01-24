using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTO.Account;
using WebAPI.DTO.User;
using WebAPI.Mapper;
using WebAPI.Models;
using WebAPI.Repository;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/users")]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _userRepository;
        public UserController(UserRepository userRepository){
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserResponseDTO>>> GetUsers()
        {
            var users = await _userRepository.GetUsersAsync();
            var userResponseDTOs = users.Select(user => user.ToUserResponse()).ToList();
            return Ok(userResponseDTOs);
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult> GetUser([FromRoute] string userId)
        {
            var user = await _userRepository.GetUserAsync(userId);
            var userResponseDTOs = user.Select(user => user.ToUserResponse());

            return Ok(userResponseDTOs);

        }

        [HttpPost]
        public async Task<ActionResult<CreateUserDTO>> CreateUser([FromBody] CreateUserDTO user)
        {

            var createUser = user.ToCreateUser(); 
            // return  await _userRepository.CreateUserAsync(createUser);
            return null;
            
        }

        [HttpPut]
        public Task<User> UpdateUser([FromBody] User user, [FromRoute] string userId)
        {
            return null; ;
        }

        [HttpDelete]
        public Task<User> DeleteUser(string userId)
        {
            return null; ;
        }
    }
}