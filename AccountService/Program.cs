using AccountService.Application;
using AccountService.Application.Interfaces;
using AccountService.Infrastructure;
using AccountService.Infrastructure.Hubs;
using AccountService.Infrastructure.Services;
using AccountService.WebApi.Extensions; 

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerExtension();

// Add SignalR
builder.Services.AddSignalR();

builder.Services.AddTransient<IAuthenticatedUserService, AuthenticatedUserService>();
builder.Services.AddApplicationLayer();
builder.Services.InfraPersistence(builder.Configuration);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy
                .AllowAnyOrigin() // Veya .WithOrigins("http://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});
var app = builder.Build();



app.UseSwagger();
    app.UseSwaggerUI();
app.MapGet("/", context =>
{
    context.Response.Redirect("/swagger");
    return Task.CompletedTask;
});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
// Map SignalR Hub
app.MapHub<NotificationHub>("/notificationHub");

app.Run();
