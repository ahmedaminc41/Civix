
using Civix.App.Core.Entities;
using Civix.App.Core.Service.Contracts.Auth;
using Civix.App.Repositories.Data;
using Civix.App.Services.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Civix.App.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddDbContext<CivixDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddIdentity<AppUser, IdentityRole>()
                            .AddEntityFrameworkStores<CivixDbContext>();




            builder.Services.AddScoped<IAuthService, AuthServic>();

            var app = builder.Build();

            // ASK CLR To Create Object From CivixDbContext
            using var scope = app.Services.CreateScope();
            var _context =  scope.ServiceProvider.GetRequiredService<CivixDbContext>();


            var loggerFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();


            Console.WriteLine("ahmed");
            Console.WriteLine("hana");

            try
            {
                await _context.Database.MigrateAsync(); // Update-database
            }
            catch (Exception ex)
            {
                var _logger = loggerFactory.CreateLogger<Program>();
                _logger.LogError(ex.Message, "error has been occured");
            }

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
