
using Cms.Services.Contract.AuthenticationService.Configuration;
using Cms.Types;
using Microsoft.Extensions.Configuration;

namespace Cms.Services.Core.AuthenticationService
{
    class AuthenticationConfigurationProvider: IAuthenticationConfigurationProvider
    {
        private IConfiguration _configuration;
        public AuthenticationConfigurationProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public JwtSettings GetApiJwtSettings()
        {
            var config = _configuration.GetSection("JwtSettings");
            return new JwtSettings { Secret = config.GetValue<string>("Secret") };
        }
    }
}
