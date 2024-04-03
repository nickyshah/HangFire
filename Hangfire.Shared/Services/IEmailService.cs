namespace Hangfire.Shared.Service
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string password);
    }
}
