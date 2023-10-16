using Logitar.EventSourcing;
using Logitar.Wishes.Contracts.Actors;

namespace Logitar.Wishes.Application.Caching;

public interface ICacheService
{
  Actor? GetActor(ActorId id);
  void SetActor(Actor actor);
}
