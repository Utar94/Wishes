using Logitar.EventSourcing;
using Logitar.Portal.Contracts.ApiKeys;
using Logitar.Portal.Contracts.Users;
using Logitar.Wishes.Contracts.Actors;

namespace Logitar.Wishes.Application.Caching;

public interface ICacheService
{
  Actor? GetActor(ActorId id);
  void SetActor(Actor actor);

  ApiKey? GetApiKey(string xApiKey);
  void SetApiKey(string xApiKey, ApiKey apiKey);

  User? GetUser(string username, string password);
  void SetUser(string username, string password, User user);
}
