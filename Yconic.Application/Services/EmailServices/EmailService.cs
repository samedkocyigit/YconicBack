using MimeKit;
using System.Net.Mail;


namespace Yconic.Application.Services.EmailServices
{
    public class EmailService:IEmailService
    {
        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("no-reply@yConic.com"));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;

            var builder = new BodyBuilder { HtmlBody = body };
            email.Body = builder.ToMessageBody();

            using var smtp = new MailKit.Net.Smtp.SmtpClient(); // Explicitly use MailKit's SmtpClient
            try
            {
                // Connect to Mailtrap
                await smtp.ConnectAsync("smtp.mailtrap.io", 587, MailKit.Security.SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync("045ec6f8b10b0e", "ab70b8e03bdbbc");
                await smtp.SendAsync(email);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to send email: {ex.Message}");
            }
            finally
            {
                await smtp.DisconnectAsync(true);
            }
        }

    }
}
