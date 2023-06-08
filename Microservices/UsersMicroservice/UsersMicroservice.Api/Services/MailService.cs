using UsersMicroservice.Domain.Abstractions;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using System.Net.Mail;
using UsersMicroservice.Api.Utils;

namespace UsersMicroservice.Api.Services
{
    public class MailService : IMailService
    {
        private readonly IConfiguration _configuration;
        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public void SendMail(string delivererMail, string firstName, string lastName)
        {
            var email = new MimeMessage();
            email.From.Add((MailboxAddress)new MailAddress(_configuration["mail"], "Delivery"));
            email.To.Add(MailboxAddress.Parse(delivererMail));
            email.Subject = "Account settings";
            email.Body = new TextPart(TextFormat.Html) { Text = $"Pozdrav {firstName} {lastName}/n, Vaš nalog je aktiviran./nSrdačan pozdrav." };

            var smtp = new MailKit.Net.Smtp.SmtpClient();
            smtp.AuthenticationMechanisms.Remove("XOAUTH2");
            smtp.Connect("smtp.gmail.com",587 , SecureSocketOptions.StartTls);
            try
            {
                smtp.Authenticate(_configuration["mail"], _configuration["password"]);
                smtp.Send(email);
            }
            catch { }
            finally
            {
                smtp.Disconnect(true);
            }
        }
    }
}
