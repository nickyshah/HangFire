using Hangfire.ProducerAPI.Service.IService;
using Hangfire.Server.options;
using Hangfire.Server.Service.IService;
using Microsoft.Extensions.Options;

namespace Hangfire.ProducerAPI.Service
{
    public class SendEmailJob : ISendEmailJob
    {
        private readonly IEmailService _emailService;

        private readonly string _email;
        private readonly string _password;
        public SendEmailJob(IOptions<ServerOptions> serverOptions, IEmailService emailService)
        {
            _emailService = emailService;
            _email = serverOptions.Value.Email;
            _password = serverOptions.Value.Password;
        }

        public Task Execute()
        {
            _emailService.SendEmail(_email, _password);
        }
    }
}
