using System.Net.Mail;
using WebAPI.Data;
using WebAPI.Interface;
using WebAPI.Models;

namespace WebAPI.Repository
{
    public class EmailRepository : IEmailRepository
    {
        private readonly SmtpClient _smtpClient;
        private readonly ApplicationDBContext _dbContext;

        private readonly IConfiguration _config;

        public EmailRepository(SmtpClient smtpClient, ApplicationDBContext dbContext, IConfiguration config)
        {
            _smtpClient = smtpClient;
            _dbContext = dbContext;
            _config = config;

        }



        public async Task SendEmailAsync(Email email)
        {


            var mailMessage = new MailMessage
            {
                From = new MailAddress(email.From),
                To = { email.To },
                Subject = email.Subject,
                Body = email.Body,
                IsBodyHtml = true,
                DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure,
                Priority = MailPriority.High,
            };
            mailMessage.ReplyToList.Add(new MailAddress(email.From));


            await _smtpClient.SendMailAsync(mailMessage);
        }

        public async Task SaveEmailAsync(Email email)
        {
            _dbContext.Emails.Add(email);
            await _dbContext.SaveChangesAsync();
        }
    }
}