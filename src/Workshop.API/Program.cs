using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Workshop.API.Extensions;
using Workshop.API.Models;
using Workshop.API.Providers;
using Workshop.API.Services;

namespace Workshop.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            string AllowReactUI = "allowReactUI";
            builder.AddDb();
            builder.AddAuthentication();
            builder.AddMailService();
            builder.Services.AddCors(AllowReactUI);
            builder.Services.AddIdentity();
            builder.Services.AddFluentValidation();
            builder.Services.AddSingleton<ITokenService, TokenService>();
            builder.Services.AddScoped<IUserRepositoryService, UserRepositoryService>();
            builder.Services.AddTransient<EmailConfirmationTokenProvider<WorkshopUser>>();
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors(AllowReactUI);

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}