using Logitar.Portal.Contracts.ApiKeys;
using Logitar.Portal.Contracts.Users;
using Logitar.Wishes.Web.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace Logitar.Wishes.Web.Authorization;

internal class RoleAuthorizationHandler : AuthorizationHandler<RoleAuthorizationRequirement>
{
  private readonly IHttpContextAccessor _httpContextAccessor;

  public RoleAuthorizationHandler(IHttpContextAccessor httpContextAccessor)
  {
    _httpContextAccessor = httpContextAccessor;
  }

  protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleAuthorizationRequirement requirement)
  {
    if (_httpContextAccessor.HttpContext != null)
    {
      User? user = _httpContextAccessor.HttpContext.GetUser();
      if (user != null)
      {
        if (user.Roles.Any(role => requirement.IsEqual(role.UniqueName)))
        {
          context.Succeed(requirement);
        }
        else
        {
          context.Fail(new AuthorizationFailureReason(this, $"The user does not have the role '{requirement.UniqueName}'."));
        }
      }

      ApiKey? apiKey = _httpContextAccessor.HttpContext.GetApiKey();
      if (apiKey != null)
      {
        if (apiKey.Roles.Any(role => requirement.IsEqual(role.UniqueName)))
        {
          context.Succeed(requirement);
        }
        else
        {
          context.Fail(new AuthorizationFailureReason(this, $"The API key does not have the role '{requirement.UniqueName}'."));
        }
      }
    }

    return Task.CompletedTask;
  }
}
