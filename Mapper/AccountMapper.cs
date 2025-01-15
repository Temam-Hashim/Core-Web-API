using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.DTO.Account;
using WebAPI.Models;

namespace WebAPI.Mapper
{
    public static class AccountMapper
    {

        public static User ToRegisterDTO(this RegisterDTO registerDto)
        {
            return new User
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                PhoneNumber = registerDto.PhoneNumber
            };
        }
    }
}