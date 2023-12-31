﻿using Logitar.Portal.Contracts.Users;
using Logitar.Wishes.Application.Caching;
using Logitar.Wishes.Contracts.Constants;
using Logitar.Wishes.Web.Extensions;
using Logitar.Wishes.Web.Settings;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace Logitar.Wishes.Web.Authentication;

internal class BasicAuthenticationHandler : AuthenticationHandler<BasicAuthenticationOptions>
{
  private readonly ICacheService _cacheService;
  private readonly IdentitySettings _identitySettings;
  private readonly IUserService _userService;

  public BasicAuthenticationHandler(IOptionsMonitor<BasicAuthenticationOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock,
    ICacheService cacheService, IdentitySettings identitySettings, IUserService userService)
      : base(options, logger, encoder, clock)
  {
    _cacheService = cacheService;
    _identitySettings = identitySettings;
    _userService = userService;
  }

  protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
  {
    if (Context.Request.Headers.TryGetValue(Headers.Authorization, out StringValues authorization))
    {
      string? value = authorization.Single();
      if (!string.IsNullOrWhiteSpace(value))
      {
        string[] values = value.Split();
        if (values.Length != 2)
        {
          return AuthenticateResult.Fail($"The {Headers.Authorization} header value is not valid: '{value}'.");
        }
        else if (values[0] == Schemes.Basic)
        {
          byte[] bytes = Convert.FromBase64String(values[1]);
          string credentials = Encoding.UTF8.GetString(bytes);
          int index = credentials.IndexOf(':');
          if (index <= 0)
          {
            return AuthenticateResult.Fail($"The {Schemes.Basic} credentials are not valid: '{credentials}'.");
          }
          string username = credentials[..index];
          string password = credentials[(index + 1)..];

          try
          {
            AuthenticateUserPayload payload = new()
            {
              Realm = _identitySettings.Realm,
              UniqueName = username,
              Password = password
            };
            User user = _cacheService.GetUser(username, password) ?? await _userService.AuthenticateAsync(payload);
            _cacheService.SetUser(username, password, user);

            Context.SetUser(user);

            ClaimsPrincipal principal = new(user.CreateClaimsIdentity(Scheme.Name));
            AuthenticationTicket ticket = new(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
          }
          catch (Exception exception)
          {
            return AuthenticateResult.Fail(exception);
          }
        }
      }
    }

    return AuthenticateResult.NoResult();
  }
}
