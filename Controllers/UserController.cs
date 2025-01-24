using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTO.User;
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
        public Task<List<UserResponseDTO>> GetUsers()
        {
            return _userRepository.GetUsersAsync();
        }
        [HttpGet("{userId}")]
        public Task<User> GetUser([FromRoute] string userId)
        {
            return null;

        }

        [HttpPost]
        public Task<User> CreateUser(User user)
        {
            return null; ;
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