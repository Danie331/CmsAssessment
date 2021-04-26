using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Cms.Data.DAL.Contract;
using Cms.Data.DAL.Core;

namespace Cms.Data.DAL
{
    public static class Registration
    {
        public static void RegisterDAL(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IStockDatastore, StockDatastore>();

            services.AddDbContext<Context.CmsStockManagmentContext>(options => options.UseSqlServer(configuration.GetConnectionString("CmsStockManagement")), ServiceLifetime.Transient);
        }
    }
}