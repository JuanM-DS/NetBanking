using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NetBanking.Core.Interfaces.Persistence;
using NetBanking.Core.Interfaces.Services;
using NetBanking.Core.Options;
using NetBanking.Core.Services;
using NetBanking.Infrastructure.Data;
using NetBanking.Infrastructure.Persistence.UnitOfWork;
namespace NetBanking.Infrastructure.ExtensionMethods
{
    public static class ServicesCollectionExtension
    {
        public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configurations)
        {
            services.AddDbContext<NetBankingDbContext>(provider => provider.UseSqlServer(
                configurations.GetConnectionString("SqlConectionString")
                ).EnableSensitiveDataLogging()
           .LogTo(Console.WriteLine, LogLevel.Information));

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IUserServices, UserServices>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<ICheckServices, CheckServices>();
            return services;
        }

        public static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<PaginationOptions>(option => configuration.GetSection("PaginationConfigurations").Bind(option));

            return services;
        }
    }
}
