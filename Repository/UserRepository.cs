using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.DTO.User;
using WebAPI.Interface;
using WebAPI.Models;

namespace WebAPI.Repository
{
    public class UserRepository : IUserRepository
    {

        private readonly ApplicationDBContext _context;
        public UserRepository(ApplicationDBContext context){
            _context = context;
        }
        public async Task<List<UserResponseDTO>> GetUsersAsync()
        {
            var userList = await _context.Users.ToListAsync();
            return (List<UserResponseDTO>)userList.Select(u=>new UserResponseDTO()); ;

        }
        public Task<List<User>> GetUserAsync()
        {
            throw new NotImplementedException();
        }

    
        public Task<List<User>> CreateUserAsync(User user)
        {
            throw new NotImplementedException();
        }


        public Task<List<User>> UpdateUserAsync(User user, string userId)
        {
            throw new NotImplementedException();
        }
        public Task<List<User>> DeleteUserAsync(string userId)
        {
            throw new NotImplementedException();
        }

       

    }
}