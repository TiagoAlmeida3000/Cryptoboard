using CryptoBoard.Application.Interfaces;
using CryptoBoard.Application.Mappings;
using CryptoBoard.Application.Services;
using CryptoBoard.Domain.Interfaces;
using CryptoBoard.Infra.Data.Context;
using CryptoBoard.Infra.Data.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoBoard.Infra.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
          IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
            b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddCors(options =>
            {
                options.AddPolicy("EnableCORS", builder =>
                {
                    builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
             .AddJwtBearer(options =>
             {
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = true,

                     ValidateAudience = true,

                     ValidateLifetime = true,

                     ValidateIssuerSigningKey = true,

                     ValidIssuer = "https://localhost:5001",

                     ValidAudience = "https://localhost:5001",

                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"))
                 };
             });

            services.AddScoped<IBinanceRepository, BinanceRepository>();

            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IBinanceService, BinanceService>();

            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IJWTService, JWTService>();

            services.AddAutoMapper(typeof(DomainToDToMappingProfile));

            return services;
        }
    }
}
