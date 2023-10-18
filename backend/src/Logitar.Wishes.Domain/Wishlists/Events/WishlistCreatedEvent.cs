using Logitar.EventSourcing;
using Logitar.Wishes.Domain.ValueObjects;
using MediatR;

namespace Logitar.Wishes.Domain.Wishlists.Events;

public record WishlistCreatedEvent : DomainEvent, INotification
{
  public DisplayNameUnit DisplayName { get; }

  public WishlistCreatedEvent(ActorId actorId, DisplayNameUnit displayName)
  {
    ActorId = actorId;
    DisplayName = displayName;
  }
}
