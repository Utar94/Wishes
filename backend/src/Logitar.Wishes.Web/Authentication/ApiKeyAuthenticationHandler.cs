using Logitar.Portal.Contracts.ApiKeys;
using Logitar.Wishes.Application.Caching;
using Logitar.Wishes.Contracts.Constants;
using Logitar.Wishes.Web.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace Logitar.Wishes.Web.Authentication;

internal class ApiKeyAuthenticationHandler : AuthenticationHandler<ApiKeyAuthenticationOptions>
{
  private readonly IApiKeyService _apiKeyService;
  private readonly ICacheService _cacheService;

  public ApiKeyAuthenticationHandler(IOptionsMonitor<ApiKeyAuthenticationOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock,
    IApiKeyService apiKeyService, ICacheService cacheService)
      : base(options, logger, encoder, clock)
  {
    _apiKeyService = apiKeyService;
    _cacheService = cacheService;
  }

  protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
  {
    if (Context.Request.Headers.TryGetValue(Headers.XApiKey, out StringValues values))
    {
      string? value = values.Single();
      if (!string.IsNullOrWhiteSpace(value))
      {
        try
        {
          ApiKey apiKey = _cacheService.GetApiKey(value) ?? await _apiKeyService.AuthenticateAsync(value);
          _cacheService.SetApiKey(value, apiKey);

          Context.SetApiKey(apiKey);

          ClaimsPrincipal principal = new(apiKey.CreateClaimsIdentity(Scheme.Name));
          AuthenticationTicket ticket = new(principal, Scheme.Name);

          return AuthenticateResult.Success(ticket);
        }
        catch (Exception exception)
        {
          return AuthenticateResult.Fail(exception);
        }
      }
    }

    return AuthenticateResult.NoResult();
  }
}
