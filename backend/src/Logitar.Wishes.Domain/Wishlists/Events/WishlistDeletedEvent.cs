using Logitar.EventSourcing;
using MediatR;

namespace Logitar.Wishes.Domain.Wishlists.Events;

public record WishlistDeletedEvent : DomainEvent, INotification
{
  public WishlistDeletedEvent(ActorId actorId)
  {
    ActorId = actorId;
    IsDeleted = true;
  }
}
