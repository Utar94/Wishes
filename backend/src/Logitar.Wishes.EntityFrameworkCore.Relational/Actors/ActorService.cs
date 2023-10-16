using Logitar.EventSourcing;
using Logitar.Wishes.Application.Actors;
using Logitar.Wishes.Application.Caching;
using Logitar.Wishes.Contracts.Actors;
using Logitar.Wishes.EntityFrameworkCore.Relational.Entities;
using Microsoft.EntityFrameworkCore;

namespace Logitar.Wishes.EntityFrameworkCore.Relational.Actors;

internal class ActorService : IActorService
{
  private readonly DbSet<ActorEntity> _actors;
  private readonly ICacheService _cacheService;

  public ActorService(ICacheService cacheService, WishesContext context)
  {
    _actors = context.Actors;
    _cacheService = cacheService;
  }


  public async Task<IEnumerable<Actor>> FindAsync(IEnumerable<ActorId> ids, CancellationToken cancellationToken)
  {
    Dictionary<ActorId, Actor> actors = new();

    HashSet<ActorId> missingIds = new();
    foreach (ActorId id in ids)
    {
      Actor? actor = _cacheService.GetActor(id);
      if (actor != null)
      {
        actors[id] = actor;
        _cacheService.SetActor(actor);
      }
      else if (id != default)
      {
        missingIds.Add(id);
      }
    }

    if (missingIds.Any())
    {
      HashSet<string> actorIds = missingIds.Select(id => id.Value).ToHashSet();
      ActorEntity[] entities = await _actors.AsNoTracking()
        .Where(actor => actorIds.Contains(actor.Id))
        .ToArrayAsync(cancellationToken);
      foreach (ActorEntity entity in entities)
      {
        Actor actor = entity.ToActor();
        ActorId id = new(actor.Id);

        actors[id] = entity.ToActor();
        _cacheService.SetActor(actor);
      }
    }

    return actors.Values;
  }
}
