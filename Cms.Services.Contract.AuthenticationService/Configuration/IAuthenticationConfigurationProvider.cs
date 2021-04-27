
using Cms.Types;

namespace Cms.Services.Contract.AuthenticationService.Configuration
{
    public interface IAuthenticationConfigurationProvider
    {
        JwtSettings GetApiJwtSettings();
        Credential GetTestCredentials();
    }
}
