
using Hangfire;
using Hangfire.Server.options;

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

builder.Services.Configure<ServerOptions>(configuration.GetSection(ServerOptions.ServerOptionKey));

var host = builder.Build();
host.Run();
