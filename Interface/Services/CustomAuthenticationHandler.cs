using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Security.Principal;
using System.Text.Encodings.Web;
using WebProjectAPI.Interface;

#nullable disable
namespace WebProjectAPI.Services
{
    public class CustomAuthenticationHandler : AuthenticationHandler<BasicAuthenticationOptions>
    {
        private readonly ICustomAuthentication _customAuthentication;
        public CustomAuthenticationHandler(
            IOptionsMonitor<BasicAuthenticationOptions> options, 
            ILoggerFactory logger, 
            UrlEncoder encoder, 
            ISystemClock clock,
            ICustomAuthentication customAuthentication) : 
            base(options, logger, encoder, clock)
        {
            _customAuthentication = customAuthentication;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Unauthorized");

            string authorizationHeader = Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(authorizationHeader))
            {
                return AuthenticateResult.NoResult();
            }

            if (!authorizationHeader.StartsWith("bearer", StringComparison.OrdinalIgnoreCase))
            {
                return AuthenticateResult.Fail("Unauthorized");
            }

            string token = authorizationHeader.Substring("bearer".Length).Trim();

            if (string.IsNullOrEmpty(token))
            {
                return AuthenticateResult.Fail("Unauthorized");
            }

            try
            {
                return validateToken(token);
            }
            catch (Exception ex)
            {
                return AuthenticateResult.Fail(ex.Message);
            }
        }

        private AuthenticateResult validateToken(string token)
        {
            var validatedToken = _customAuthentication.Tokens.FirstOrDefault(t => t.Key == token);
            if (validatedToken.Key == null)
            {
                return AuthenticateResult.Fail("Unauthorized");
            }
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, validatedToken.Value.Item1),
                    new Claim(ClaimTypes.Role, validatedToken.Value.Item2)
                };

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new GenericPrincipal(identity, new[] { validatedToken.Value.Item2 });
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            return AuthenticateResult.Success(ticket);
        }
    }
}
