
using Civix.App.Core.Entities;
using Civix.App.Core.Service.Contracts.Auth;
using Civix.App.Core.Service.Contracts.Token;
using Civix.App.Repositories.Data;
using Civix.App.Services.Auth;
using Civix.App.Services.Token;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;


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
            builder.Services.AddScoped<IToken, TokenService>();

            var jwtSettings = builder.Configuration.GetSection("JWT");
            builder.Services.AddAuthentication("Bearer")
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings.GetValue<string>("ValidIssuer"),
                        ValidAudience = jwtSettings.GetValue<string>("ValidAudience"),
                        IssuerSigningKey = new SymmetricSecurityKey( Encoding.UTF8.GetBytes(jwtSettings.GetValue<string>("Key")))
                       
                    };
                });




            var app = builder.Build();

            // ASK CLR To Create Object From CivixDbContext
            using var scope = app.Services.CreateScope();
            var _context =  scope.ServiceProvider.GetRequiredService<CivixDbContext>();

            var loggerFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();
            
//<<<<<<< HEAD
            Console.WriteLine("Shiref");
            Console.WriteLine("ahmed");
            Console.WriteLine("hana");

//=======
//>>>>>>> 9ac919796cdd68903b8a32b55282ec5b93253719
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
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
