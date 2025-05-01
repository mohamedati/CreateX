using Application.Services;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace API.Extentions
{
    public class EmailHealthCheckPublisher : IHealthCheckPublisher
    {
        private readonly IEmailSender _emailSender;

        public EmailHealthCheckPublisher(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public async Task PublishAsync(HealthReport report, CancellationToken cancellationToken)
        {
            if (report.Status == HealthStatus.Unhealthy)
            {
                var message = string.Join("\n", report.Entries.Select(e => $"{e.Key}: {e.Value.Status}"));
                await _emailSender.SendEmailAsync("atiffahmykhamis@gmail.com", "Health Check Alert", message);
            }


        }
    }
}
