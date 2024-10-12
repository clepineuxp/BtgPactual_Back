using BtgPactual.Back.Domain.Configurations;
using Microsoft.Extensions.Options;

namespace BtgPactual.Back.Api.Extensions
{
    public static class ConfigurationExtensions
    {
        public static void AddConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DatabaseConfiguration>(configuration.GetSection("DatabaseConfiguration"));
            services.Configure<EmailConfiguration>(configuration.GetSection("EmailConfiguration"));
            services.Configure<SmsConfiguration>(configuration.GetSection("SmsConfiguration"));


            services.AddSingleton(sp => sp.GetService<IOptions<DatabaseConfiguration>>()!.Value);
            services.AddSingleton(sp => sp.GetService<IOptions<EmailConfiguration>>()!.Value);
            services.AddSingleton(sp => sp.GetService<IOptions<SmsConfiguration>>()!.Value);
        }
    }
}
