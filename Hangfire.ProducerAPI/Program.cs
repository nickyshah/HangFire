using Hangfire;
using Hangfire.Shared.Service;
using HangfireBasicAuthenticationFilter;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHangfire(opt =>
{
    opt.UseSqlServerStorage(builder.Configuration.GetConnectionString("DbConnectionString"))
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHangfireDashboard("/hangfire", new DashboardOptions
{
    Authorization = new[]
    {
        new HangfireCustomBasicAuthenticationFilter
        {
            User = app.Configuration.GetSection("HangfireOptions:User").Value,
            Pass = app.Configuration.GetSection("HangfireOptions:Pass").Value
        }
    }
});

RecurringJob.AddOrUpdate<ISendEmailJob>(Guid.NewGuid().ToString(), x => x.Execute(), Cron.Minutely);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
