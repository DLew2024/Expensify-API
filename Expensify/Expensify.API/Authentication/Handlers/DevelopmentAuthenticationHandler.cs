using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using static Expensify.DataAccessLayer.Utility.Constants;

namespace Expensify.API.Authentication.Handlers;

public class DevelopmentAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    public DevelopmentAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder
    )
        : base(options, logger, encoder) { }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, DevelopmentUsers.UserId.ToString()),
            new Claim(ClaimTypes.Email, DevelopmentUsers.Email),
            new Claim(ClaimTypes.Role, RoleIds.Admin.ToString()),
        };

        var identity = new ClaimsIdentity(claims, Scheme.Name);

        var principal = new ClaimsPrincipal(identity);

        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }

    public static class DevelopmentUsers
    {
        public static readonly Guid UserId = new("88CD3AEE-9FB0-45E5-981E-3D40FF9DA11E");
        public const string Email = "developer@local.dev";
    }
}
