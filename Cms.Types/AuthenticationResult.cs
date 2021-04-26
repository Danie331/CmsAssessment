
namespace Cms.Types
{
    public class AuthenticationResult
    {
        public string Token { get; set; }
        public bool AuthenticationSuccess { get; set; }
        public string AuthenticationFailureReason { get; set; }
    }
}
