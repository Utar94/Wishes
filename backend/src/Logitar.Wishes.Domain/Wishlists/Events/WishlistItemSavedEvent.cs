using Logitar.EventSourcing;
using MediatR;

namespace Logitar.Wishes.Domain.Wishlists.Events;

public record WishlistItemSavedEvent : DomainEvent, INotification
{
  public ItemId ItemId { get; }
  public ItemUnit Item { get; }

  public WishlistItemSavedEvent(ActorId actorId, ItemId itemId, ItemUnit item)
  {
    ActorId = actorId;
    ItemId = itemId;
    Item = item;
  }
}
