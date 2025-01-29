using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.DTO.User;
using WebAPI.Interface;
using WebAPI.Mapper;
using WebAPI.Models;
using WebAPI.Service;

namespace WebAPI.Repository
{
    public class UserRepository : IUserRepository
    {

        private readonly ApplicationDBContext _context;
        private readonly UserManager<User> _userManager;
        public UserRepository(ApplicationDBContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<List<User>> GetUsersAsync()
        {

            return await _context.Users.ToListAsync();
            // return await _context.Users
            //             .Select(user => user.ToUserResponse())
            //             .ToListAsync(); // EF Core's async materialization

        }
        public async Task<object> GetUserAsync(string id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
                return ApiResponseService.Error(403, "No user found with the specified ID.");

            return user;
        }

        public async Task<IActionResult> CreateUserAsync(CreateUserDTO userDTO, string profilePicture)
        {
            try
            {
                // Map DTO to User model
                var user = userDTO.ToCreateUser();

                // Set the profile image path if available
                if (!string.IsNullOrEmpty(profilePicture))
                {
                    user.ProfilePicture= profilePicture;
                }

                // Create the user
                var createResult = await _userManager.CreateAsync(user, userDTO.Password);
                if (!createResult.Succeeded)
                {
                    return ApiResponseService.Error(500, "User creation failed.", createResult.Errors);
                }

                // Assign the default role to the user
                var roleResult = await _userManager.AddToRoleAsync(user, "user");
                if (!roleResult.Succeeded)
                {
                    return ApiResponseService.Error(500, "Failed to assign role to the user.", roleResult.Errors);
                }

                // Map the created user to a response DTO
                var userResponse = user.ToUserResponse();
                return new OkObjectResult(userResponse);
            }
            catch (Exception ex)
            {
                return new ObjectResult(ex.Message) { StatusCode = 500 };
            }
        }
        public async Task<User> UpdateUserAsync(UpdateUserDTO updatedUser, string userId)
        {


            // Find the existing user
            var existingUser = await _context.Users.FindAsync(userId) ?? throw new KeyNotFoundException("User not found.");

            // Update the existing user using the DTO
            existingUser.UpdateFromDTO(updatedUser);

            // Save changes to the database
            _context.Users.Update(existingUser);
            await _context.SaveChangesAsync();

            return existingUser;

            // // Find the existing user
            // var existingUser = await _context.Users.FindAsync(userId) ?? throw new KeyNotFoundException("User not found.");

            // // Update only fields that are provided
            // existingUser.UserName = string.IsNullOrWhiteSpace(updatedUser.UserName) ? existingUser.UserName : updatedUser.UserName;
            // existingUser.Email = string.IsNullOrWhiteSpace(updatedUser.Email) ? existingUser.Email : updatedUser.Email;
            // existingUser.FirstName = string.IsNullOrWhiteSpace(updatedUser.FirstName) ? existingUser.FirstName : updatedUser.FirstName;
            // existingUser.LastName = string.IsNullOrWhiteSpace(updatedUser.LastName) ? existingUser.LastName : updatedUser.LastName;
            // existingUser.PhoneNumber = string.IsNullOrWhiteSpace(updatedUser.PhoneNumber) ? existingUser.PhoneNumber : updatedUser.PhoneNumber;

            // // Save changes to the database
            // _context.Users.Update(existingUser);
            // await _context.SaveChangesAsync();

            // return existingUser;

        }


        public async Task<object> DeleteUserAsync(string userId)
        {
            var existingUser = await _context.Users.FindAsync(userId);
            if(existingUser == null) {
                return new {
                    StatusCode = 500,
                    ErrorMessage = "userId is not a valid user",
                    status = "failed"
                };
            }
            _context.Users.Remove(existingUser);
            await _context.SaveChangesAsync();

            return new
            {
                Message = "User deleted successfully.",
                User = existingUser.ToUserResponse()
            };
        }

        public async Task<bool> ChangePasswordAsync(string userId, string currentPassword, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;

            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);

            return result.Succeeded;
        }



    }
}