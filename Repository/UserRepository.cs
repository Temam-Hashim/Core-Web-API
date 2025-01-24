using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.DTO.User;
using WebAPI.Interface;
using WebAPI.Mapper;
using WebAPI.Models;

namespace WebAPI.Repository
{
    public class UserRepository : IUserRepository
    {

        private readonly ApplicationDBContext _context;
        public UserRepository(ApplicationDBContext context){
            _context = context;
        }
        public async Task<List<User>> GetUsersAsync()
        {

            return await _context.Users.ToListAsync();
            // return await _context.Users
            //             .Select(user => user.ToUserResponse())
            //             .ToListAsync(); // EF Core's async materialization

        }
        public Task<List<User>> GetUserAsync(string id)
        {
           return  _context.Users.Where(u => u.Id == id).ToListAsync();
        }

    
        public async Task<User> CreateUserAsync(User user)
        {
             await _context.Users.AddAsync(user);
             await _context.SaveChangesAsync();
             return user;

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