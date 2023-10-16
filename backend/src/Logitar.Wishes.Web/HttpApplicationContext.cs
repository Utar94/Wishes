using Logitar.EventSourcing;
using Logitar.Wishes.Application;
using Logitar.Wishes.Contracts.Actors;

namespace Logitar.Wishes.Web;

internal class HttpApplicationContext : IApplicationContext
{
  public Actor Actor { get; } = new(); // TODO(fpion): Authentication
  public ActorId ActorId => new(Actor.Id);
}
