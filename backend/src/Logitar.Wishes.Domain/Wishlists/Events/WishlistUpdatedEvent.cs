using Logitar.EventSourcing;
using Logitar.Wishes.Contracts;
using Logitar.Wishes.Domain.ValueObjects;
using MediatR;

namespace Logitar.Wishes.Domain.Wishlists.Events;

public record WishlistUpdatedEvent : DomainEvent, INotification
{
  public DisplayNameUnit? DisplayName { get; set; }
  public Modification<UrlUnit>? PictureUrl { get; set; }

  public bool HasChanges => DisplayName != null || PictureUrl != null;
}
