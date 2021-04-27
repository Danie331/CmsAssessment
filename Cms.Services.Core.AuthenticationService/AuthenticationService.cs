
using Cms.Services.Contract.AuthenticationService;
using Cms.Services.Contract.AuthenticationService.Configuration;
using Cms.Types;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Cms.Services.Core.AuthenticationService
{
    class AuthenticationService : IAuthenticationService
    {
        private readonly IAuthenticationConfigurationProvider _configurationProvider;
        public AuthenticationService(IAuthenticationConfigurationProvider configurationProvider)
        {
            _configurationProvider = configurationProvider;
        }

        public AuthenticationResult Authenticate(string username, string password)
        {
            var testCredentials = _configurationProvider.GetTestCredentials();
            if (username == testCredentials.Username && password == testCredentials.Password)
            {
                return new AuthenticationResult { AuthenticationSuccess = true, Token = GenerateJwtForTestUser() };
            }

            return new AuthenticationResult
            {
                AuthenticationSuccess = false,
                AuthenticationFailureReason = "Bad username/password"
            };
        }

        private string GenerateJwtForTestUser()
        {
            var testCredentials = _configurationProvider.GetTestCredentials();
            var jwtSettings = _configurationProvider.GetApiJwtSettings();
            var key = Encoding.ASCII.GetBytes(jwtSettings.Secret);
            var signingKey = new SymmetricSecurityKey(key);

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, testCredentials.Username, ClaimValueTypes.String)
                    }),
                SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
