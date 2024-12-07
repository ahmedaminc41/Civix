
using Civix.App.Core.Entities;
using Civix.App.Core.Service.Contracts.Auth;
using Civix.App.Core.Service.Contracts.Token;
using Civix.App.Repositories.Data;
using Civix.App.Services.Auth;
using Civix.App.Services.Token;
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
                            .AddEntityFrameworkStores<CivixDbContext>()
                            .AddDefaultTokenProviders();



            builder.Services.AddScoped<IAuthService, AuthServic>();

            var app = builder.Build();

            // ASK CLR To Create Object From CivixDbContext
            using var scope = app.Services.CreateScope();
            var _context =  scope.ServiceProvider.GetRequiredService<CivixDbContext>();

            var loggerFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();
            builder.Services.AddScoped<IToken, TokenService>();
<<<<<<< HEAD
            Console.WriteLine("Shiref");
            Console.WriteLine("ahmed");
            Console.WriteLine("hana");

=======
>>>>>>> 9ac919796cdd68903b8a32b55282ec5b93253719
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
