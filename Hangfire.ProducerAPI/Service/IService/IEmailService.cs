namespace Hangfire.ProducerAPI.Service.IService
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string password);
    }
}
