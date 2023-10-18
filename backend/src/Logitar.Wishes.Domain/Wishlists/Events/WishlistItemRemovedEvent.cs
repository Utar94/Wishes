using Logitar.EventSourcing;
using MediatR;

namespace Logitar.Wishes.Domain.Wishlists.Events;

public record WishlistItemRemovedEvent : DomainEvent, INotification
{
  public ItemId ItemId { get; }

  public WishlistItemRemovedEvent(ActorId actorId, ItemId itemId)
  {
    ActorId = actorId;
    ItemId = itemId;
  }
}
