using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTO.User;
using WebAPI.Models;

namespace WebAPI.Interface
{
    public interface IUserRepository
    {
        Task<List<User>> GetUsersAsync();
        Task<object> GetUserAsync(string id);
        Task<IActionResult> CreateUserAsync(CreateUserDTO user, string profilePicture);
        Task<User> UpdateUserAsync(UpdateUserDTO user, string userId);
        Task<object> DeleteUserAsync(string userId);
        Task<bool> ChangePasswordAsync(string userId, string currentPassword, string newPassword);
    }
}