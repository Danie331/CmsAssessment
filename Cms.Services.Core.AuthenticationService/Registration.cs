using Cms.Services.Contract.AuthenticationService;
using Cms.Services.Contract.AuthenticationService.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Cms.Services.Core.AuthenticationService
{
    public static class Registration
    {
        public static void RegisterAuthenticationService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IAuthenticationConfigurationProvider, AuthenticationConfigurationProvider>();
        }
    }
}
