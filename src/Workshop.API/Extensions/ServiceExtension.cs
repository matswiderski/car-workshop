using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Workshop.API.Data;
using Workshop.API.Models;
using Workshop.API.Providers;
using Workshop.API.Services;
using Workshop.API.Settings;

namespace Workshop.API.Extensions
{
    public static class ServiceExtension
    {
        public static void AddIdentity(this IServiceCollection services)
        {
            services.AddIdentityCore<WorkshopUser>(options =>
            {
                options.SignIn.RequireConfirmedEmail = true;
                options.User.RequireUniqueEmail = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Tokens.ProviderMap.Add("EmailConfirmation",
                    new TokenProviderDescriptor(
                            typeof(EmailConfirmationTokenProvider<WorkshopUser>)));
                options.Tokens.EmailConfirmationTokenProvider = "EmailConfirmation";
            })
                .AddEntityFrameworkStores<WorkshopDbContext>();
        }

        public static void AddAuthentication(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
                    ValidAudience = builder.Configuration["JWT:ValidAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"])),
                    ClockSkew = TimeSpan.Zero
                }
            );
        }

        public static void AddDb(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<WorkshopDbContext>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("WorkshopContext")));
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
            services.AddValidatorsFromAssemblyContaining<AuthenticationRequest>();
            services.AddValidatorsFromAssemblyContaining<RegisterRequest>();
            ValidatorOptions.Global.LanguageManager.Enabled = false;
        }

        public static void AddMailService(this WebApplicationBuilder builder)
        {
            var mailSenderConfig = builder.Configuration.GetSection("MailServiceSettings");
            mailSenderConfig["Login"] = builder.Configuration["MailService:Login"];
            mailSenderConfig["Password"] = builder.Configuration["MailService:Password"];
            builder.Services.Configure<MailServiceSettings>(mailSenderConfig);
            builder.Services.AddSingleton<IMailService, MailService>();
        }
    }
}
