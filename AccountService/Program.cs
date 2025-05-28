using AccountService.Application;
using AccountService.Application.Interfaces;
using AccountService.Infrastructure;
using AccountService.Infrastructure.Hubs;
using AccountService.Infrastructure.Services;
using AccountService.WebApi.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerExtension();

// Add SignalR
builder.Services.AddSignalR();
builder.Services.AddControllers(config =>
{
    var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
    config.Filters.Add(new AuthorizeFilter(policy));
});
builder.Services.AddTransient<IAuthenticatedUserService, AuthenticatedUserService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddApplicationLayer();
builder.Services.InfraPersistence(builder.Configuration);

// Add Google Maps Service
builder.Services.AddHttpClient<IGoogleMapsService, GoogleMapsService>();

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

app.UseCors("AllowAll");

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
