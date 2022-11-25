using Categories.API.Models;
using Categories.API.Services;

namespace Categories.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.Configure<CategoriesDatabaseSettings>(builder.Configuration.GetSection("CategoriesDatabase"));
            builder.Services.AddSingleton<ICategoriesService, CategoriesService>();

            builder.Services.AddControllers(options =>
                options.SuppressAsyncSuffixInActionNames = false
            );

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

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}