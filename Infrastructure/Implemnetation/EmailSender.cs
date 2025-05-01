using MailKit.Net.Smtp;

using MimeKit;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using Application.Services;

namespace API.Implemnetation
{
    public class EmailSender(IConfiguration _configuration) : IEmailSender
    {
      

     
    public async Task SendEmailAsync(string to, string subject, string body)
        {
            var smtpSection = _configuration.GetSection("SmtpSettings");

            var _username = smtpSection["FromEmail"];
            var _password = smtpSection["Password"];
            var _smtpServer = smtpSection["Host"];
            var _smtpPort = int.Parse(smtpSection["Port"]);
            var enableSsl = bool.Parse(smtpSection["EnableSsl"]);



            var message = new MimeMessage();

            // Set sender
            message.From.Add(new MailboxAddress("Your Name", _username));

            // Set recipient
            message.To.Add(new MailboxAddress("", to));

            // Set subject and body
            message.Subject = subject;
            message.Body = new TextPart("html") { Text = body };

            // Use SmtpClient to send the message
            using (var client = new SmtpClient())
            {
                // Connect to Gmail's SMTP server
                // Connect to Gmail's SMTP server using STARTTLS on port 587
                await client.ConnectAsync(_smtpServer, _smtpPort, SecureSocketOptions.StartTls);
                // Authenticate with your email and app password
                await client.AuthenticateAsync(_username, _password);

                // Send the message
                await client.SendAsync(message);

                // Disconnect after sending the email
                await client.DisconnectAsync(true);
            }

        }
    }
}
