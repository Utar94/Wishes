using Logitar.Portal.Contracts.ApiKeys;
using Logitar.Wishes.Contracts.Constants;
using Logitar.Wishes.Web.Models.Share;
using Logitar.Wishes.Web.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Logitar.Wishes.Web.Controllers;

[ApiController]
[Route("share")]
public class ShareController : ControllerBase
{
  private readonly IApiKeyService _apiKeyService;
  private readonly IConfiguration _configuration;
  private readonly IdentitySettings _identitySettings;

  public ShareController(IApiKeyService apiKeyService, IConfiguration configuration, IdentitySettings identitySettings)
  {
    _apiKeyService = apiKeyService;
    _configuration = configuration;
    _identitySettings = identitySettings;
  }

  [Authorize(Policy = Policies.CanShareWishlists)]
  [HttpPost]
  public async Task<ActionResult<SharedLink>> ShareAsync([FromBody] CreateShareLinkPayload sharePayload, CancellationToken cancellationToken)
  {
    CreateApiKeyPayload createApiKeyPayload = new()
    {
      Realm = _identitySettings.Realm,
      DisplayName = sharePayload.DisplayName,
      Description = sharePayload.Description,
      ExpiresOn = sharePayload.ExpiresOn,
      Roles = new[] { _identitySettings.Roles.ReadWishlists }
    };

    ApiKey apiKey = await _apiKeyService.CreateAsync(createApiKeyPayload, cancellationToken);

    string applicationUrl = _configuration.GetValue<string>("ApplicationUrl") ?? string.Empty;
    Uri uri = new($"{applicationUrl}?key={apiKey.XApiKey}");
    SharedLink sharedLink = new(uri);

    return Created(uri, sharedLink);
  }
}
