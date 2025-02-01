using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.DTO.Email;
using WebAPI.Models;

namespace WebAPI.Mapper
{
    public static class EmailMapper
    {
        public static Email ToEntity(EmailDTO dto)
        {
            return new Email
            {
                To = dto.To,
                From = dto.From,
                Subject = dto.Subject,
                Body = dto.Body,
                SentDate = DateTime.UtcNow
            };
        }

        public static EmailDTO ToDto(Email entity)
        {
            return new EmailDTO
            {
                To = entity.To,
                From = entity.From,
                Subject = entity.Subject,
                Body = entity.Body
            };
        }
    }
}