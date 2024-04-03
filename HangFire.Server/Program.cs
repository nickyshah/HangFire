
using Hangfire;
using Hangfire.ProducerAPI.Service;
using Hangfire.ProducerAPI.Service.IService;
using Hangfire.Server.options;
using Hangfire.Server.Service.IService;

var builder = Host.CreateApplicationBuilder(args);
var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

builder.Services.AddHangfire(opt =>
{
    opt.UseSqlServerStorage(configuration.GetConnectionString("DbConnectionString"))
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings();
});

builder.Services.AddHangfireServer();
builder.Services.AddScoped<ISendEmailJob, SendEmailJob>();
builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.Configure<ServerOptions>(configuration.GetSection(ServerOptions.ServerOptionKey));

var host = builder.Build();
host.Run();
