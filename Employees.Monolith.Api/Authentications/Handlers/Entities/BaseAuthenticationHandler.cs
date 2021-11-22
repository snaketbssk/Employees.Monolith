using Employees.Monolith.DataLayer.Models.Contexts.Entities;
using Employees.Monolith.LogicLayer.Models.Constants;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Employees.Monolith.Api.Authentications.Handlers.Entities
{
    public abstract class BaseAuthenticationHandler<TOptions> : AuthenticationHandler<TOptions>
        where TOptions : AuthenticationSchemeOptions, new()
    {
        protected readonly DatabaseContext _context;
        public BaseAuthenticationHandler(
            IOptionsMonitor<TOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            DatabaseContext context)
            : base(options, logger, encoder, clock)
        {
            _context = context;
        }
        protected sealed override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey(HeaderConstant.X_TOKEN))
            {
                return await Task.FromResult(AuthenticateResult.Fail($"{DateTime.UtcNow} Header not found."));
            }
            IEnumerable<Claim> claims = null;
            try
            {
                var tokenHeader = Request.Headers[HeaderConstant.X_TOKEN].ToString();
                claims = await GetClaimsAsync(tokenHeader);
            }
            catch (Exception)
            {
                return await Task.FromResult(AuthenticateResult.Fail($"{DateTime.UtcNow} Token parse exception"));
            }
            var claimsIdentity = new ClaimsIdentity(claims, nameof(BaseAuthenticationHandler<TOptions>));
            var authenticationTicket = new AuthenticationTicket(new ClaimsPrincipal(claimsIdentity), Scheme.Name);
            return await Task.FromResult(AuthenticateResult.Success(authenticationTicket));
        }
        protected abstract Task<IEnumerable<Claim>> GetClaimsAsync(string token);
    }
}
