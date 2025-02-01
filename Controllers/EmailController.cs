using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTO.Email;
using WebAPI.Interface;
using WebAPI.Mapper;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/email")]
    public class EmailController : ControllerBase
    {
        private readonly IEmailRepository _emailRepository;

        public EmailController(IEmailRepository emailRepository)
        {
            _emailRepository = emailRepository;
        }

        [HttpPost("send")]
        public async Task<object> SendEmail([FromBody] EmailDTO emailDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var emailEntity = EmailMapper.ToEntity(emailDto);
           

            await _emailRepository.SendEmailAsync(emailEntity);
            await _emailRepository.SaveEmailAsync(emailEntity);

          return new {
                message="Email Sent successfully",
                status=200,
                emailEntity
          };
        }
    }
}