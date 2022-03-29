using CryptoBoard.Application.Interfaces;
using CryptoBoard.Application.Mappings;
using CryptoBoard.Application.Services;
using CryptoBoard.Domain.Interfaces;
using CryptoBoard.Infra.Data.Context;
using CryptoBoard.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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

            services.AddScoped<IBinanceRepository, BinanceRepository>();

            services.AddScoped<IBinanceService, BinanceService>();

            services.AddAutoMapper(typeof(DomainToDToMappingProfile));

            return services;
        }
    }
}
