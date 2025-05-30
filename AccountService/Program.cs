using AccountService.Application;
using AccountService.Application.Interfaces;
using AccountService.Hubs;
using AccountService.Infrastructure; 
using AccountService.Infrastructure.Services;
using AccountService.WebApi.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerExtension();

// Add SignalR
builder.Services.AddSignalR();
builder.Services.AddSingleton<AccountService.Hubs.NotificationService>();

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
            .WithOrigins("http://localhost:5173") // Frontend'in çalıştığı port
            .WithOrigins("http://localhost:5174") // Frontend'in çalıştığı port
            .WithOrigins("http://localhost:5174") // Frontend'in çalıştığı port
            .WithOrigins("https://graduation-project-hazel.vercel.app")            // Frontend'in çalıştığı port
            .WithOrigins("https://adminpaneli-logistify.onrender.com")            // Frontend'in çalıştığı port
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials(); // Credentials için bu gerekli
    });
});

var app = builder.Build();

// Global exception handler
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";

        var error = context.Features.Get<IExceptionHandlerFeature>();
        if (error != null)
        {
            var ex = error.Error;
            var errorResponse = new
            {
                StatusCode = 500,
                Message = ex.Message,
                DetailedMessage = ex.ToString(),
                Source = ex.Source,
                StackTrace = ex.StackTrace
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
        }
    });
});

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
