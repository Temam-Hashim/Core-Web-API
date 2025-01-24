using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.DTO.User;
using WebAPI.Models;

namespace WebAPI.Interface
{
    public interface IUserRepository
    {
        Task<List<UserResponseDTO>> GetUsersAsync();
        Task<List<User>> GetUserAsync();
        Task<List<User>> CreateUserAsync(User user);
        Task<List<User>> UpdateUserAsync(User user, string userId);
        Task<List<User>> DeleteUserAsync(string userId);
    }
}