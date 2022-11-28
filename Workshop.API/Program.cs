using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Workshop.API.Data;
using Workshop.API.Extensions;
using Workshop.API.Filters;

namespace Workshop.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<WorkshopDbContext>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("WorkshopContext")));
            builder.Services.ConfigureIdentity();
            builder.Services.ConfigureFluentValidation();

            builder.Services.AddControllers(options =>
            {
                options.Filters.Add(typeof(ValidateModelAttribute));
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}