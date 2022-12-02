using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Workshop.API.Data;
using Workshop.API.Filters;
using Workshop.API.Models;

namespace Workshop.API.Extensions
{
    public static class ServiceExtension
    {
        public static void AddIdentity(this IServiceCollection services)
        {
            services.AddIdentityCore<WorkshopUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.User.RequireUniqueEmail = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
            })
                .AddEntityFrameworkStores<WorkshopDbContext>();

        }

        public static void AddCors(this IServiceCollection services, string AllowReactUI)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(AllowReactUI,
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:3000")
                            .AllowAnyMethod()
                            .AllowCredentials()
                            .AllowAnyHeader();
                    });
            });
        }

        public static void AddFluentValidation(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<ValidateModelAttribute>();
            ValidatorOptions.Global.LanguageManager.Enabled = false;
        }
    }
}
