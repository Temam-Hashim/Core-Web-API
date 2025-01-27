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


        public static User ToCreateUser(this CreateUserDTO userResponse)
        {
            return new User
            {
                UserName = userResponse.UserName,
                Email = userResponse.Email,
                FirstName = userResponse.FirstName,
                LastName = userResponse.LastName,
                PhoneNumber = userResponse.PhoneNumber
            };
        }

        public static void UpdateFromDTO(this User user, UpdateUserDTO dto)
        {
            if (!string.IsNullOrWhiteSpace(dto.UserName))
                user.UserName = dto.UserName;

            if (!string.IsNullOrWhiteSpace(dto.Email))
                user.Email = dto.Email;

            if (!string.IsNullOrWhiteSpace(dto.FirstName))
                user.FirstName = dto.FirstName;

            if (!string.IsNullOrWhiteSpace(dto.LastName))
                user.LastName = dto.LastName;

            if (!string.IsNullOrWhiteSpace(dto.PhoneNumber))
                user.PhoneNumber = dto.PhoneNumber;
        }


    }
}