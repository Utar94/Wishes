using Logitar.EventSourcing;
using Logitar.Wishes.Contracts.Actors;

namespace Logitar.Wishes.Application;

public interface IApplicationContext
{
  Actor Actor { get; }
  ActorId ActorId { get; }
}
