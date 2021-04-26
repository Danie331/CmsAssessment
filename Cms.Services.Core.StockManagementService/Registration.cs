using Cms.Services.Contract.StockManagementService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cms.Services.Core.StockManagementService
{
    public static class Registration
    {
        public static void RegisterStockManagementService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IStockQueryService, StockQueryService>();
            services.AddScoped<IStockUpdateService, StockUpdateService>();

            Data.DAL.Registration.RegisterDAL(services, configuration);
        }
    }
}
