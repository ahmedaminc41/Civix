
using System.Reflection;
using System.Text;
using Civix.App.Api.Exceptions;
using Civix.App.Api.Validations;
using Civix.App.Core.Dtos.Auth;
using Civix.App.Core.Entities;
using Civix.App.Core.Service.Contracts.Auth;
using Civix.App.Core.Service.Contracts.Token;
using Civix.App.Repositories.Data;
using Civix.App.Services.Auth;
using Civix.App.Services.Token;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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
            builder.Services.AddScoped<ITokenService, TokenService>();


            builder.Services
                    .AddFluentValidationAutoValidation()
                    .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
            builder.Services.AddProblemDetails();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("MyPolicy", config =>
                {
                    config.AllowAnyOrigin();
                    config.AllowAnyHeader();
                    config.AllowAnyMethod();
                });
            });


            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                                           .AddJwtBearer(options => {

                                               options.TokenValidationParameters = new TokenValidationParameters()
                                               {
                                                   ValidateIssuer = true,
                                                   ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
                                                   ValidateAudience = true,
                                                   ValidAudience = builder.Configuration["JWT:ValidAudience"],
                                                   ValidateLifetime = true,
                                                   ValidateIssuerSigningKey = true,
                                                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
                                               };

                                           });

            var app = builder.Build();

            // ASK CLR To Create Object From CivixDbContext
            using var scope = app.Services.CreateScope();
            var _context = scope.ServiceProvider.GetRequiredService<CivixDbContext>();

            var loggerFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();

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
            app.UseCors("MyPolicy");

            app.UseAuthorization();

            app.MapControllers();

            app.UseExceptionHandler();

            app.Run();
        }
    }
}
