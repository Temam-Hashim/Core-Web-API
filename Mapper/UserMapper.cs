using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.DTO.User;
using WebAPI.Models;

namespace WebAPI.Mapper
{
    public static class UserMapper
    {
        public static UserResponseDTO ToUserResponse(this User userResponse){
            return new UserResponseDTO{
                Id = userResponse.Id,
                UserName = userResponse.UserName,
                Email = userResponse.Email,
                FirstName = userResponse.FirstName,
                LastName = userResponse.LastName,
                PhoneNumber = userResponse.PhoneNumber
            };
        }

        
    }
}