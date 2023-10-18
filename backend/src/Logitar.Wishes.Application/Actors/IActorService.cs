using Logitar.EventSourcing;
using Logitar.Wishes.Contracts.Actors;

namespace Logitar.Wishes.Application.Actors;

public interface IActorService
{
  Task<IEnumerable<Actor>> FindAsync(IEnumerable<ActorId> ids, CancellationToken cancellationToken = default);
}
