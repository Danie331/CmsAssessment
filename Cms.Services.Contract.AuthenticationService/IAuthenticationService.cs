
using Cms.Types;

namespace Cms.Services.Contract.AuthenticationService
{
    public interface IAuthenticationService
    {
        AuthenticationResult Authenticate(string username, string password);
    }
}
