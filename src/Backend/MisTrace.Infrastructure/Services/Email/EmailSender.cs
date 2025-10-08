using Microsoft.AspNetCore.Identity.UI.Services;

namespace CrewDo.Infrastructure.Services.Email
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // No hace nada
            return Task.CompletedTask;
        }
    }
}
