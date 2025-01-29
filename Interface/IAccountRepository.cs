using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTO.Account;

namespace WebAPI.Interface
{
    public interface IAccountRepository
    {
        public Task<IActionResult> Register(RegisterDTO registerDTO, string profilePicture);
        Task<IActionResult> Login(LoginDTO loginDTO);
    }
}