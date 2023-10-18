using Logitar.EventSourcing;
using Logitar.Portal.Contracts.ApiKeys;
using Logitar.Portal.Contracts.Users;
using Logitar.Wishes.Application;
using Logitar.Wishes.Contracts.Actors;
using Logitar.Wishes.Web.Extensions;

namespace Logitar.Wishes.Web;

internal class HttpApplicationContext : IApplicationContext
{
  private readonly Actor _system = new();

  private readonly IHttpContextAccessor _httpContextAccessor;
  protected HttpContext Context => _httpContextAccessor.HttpContext
    ?? throw new InvalidOperationException($"The {nameof(_httpContextAccessor.HttpContext)} is required.");

  public Actor Actor
  {
    get
    {
      User? user = Context.GetUser();
      if (user != null)
      {
        return new Actor
        {
          Id = user.Id.ToString(),
          Type = ActorType.User,
          DisplayName = user.FullName ?? user.UniqueName,
          EmailAddress = user.Email?.Address,
          PictureUrl = user.Picture
        };
      }

      ApiKey? apiKey = Context.GetApiKey();
      if (apiKey != null)
      {
        return new Actor
        {
          Id = apiKey.Id.ToString(),
          Type = ActorType.ApiKey,
          DisplayName = apiKey.DisplayName
        };
      }

      return _system;
    }
  }
  public ActorId ActorId => new(Actor.Id);

  public HttpApplicationContext(IHttpContextAccessor httpContextAccessor)
  {
    _httpContextAccessor = httpContextAccessor;
  }
}
