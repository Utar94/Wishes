using Logitar.Wishes.Domain.Wishlists;
using Logitar.Wishes.Domain.Wishlists.Events;

namespace Logitar.Wishes.EntityFrameworkCore.Relational.Entities;

internal class WishlistEntity : AggregateEntity
{
  public int WishlistId { get; private set; }

  public string DisplayName { get; private set; } = string.Empty;
  public string? PictureUrl { get; private set; }

  public int ItemCount { get; private set; }
  //public List<Item> Items { get; set; } = new(); // TODO(fpion): wishlist items

  public WishlistEntity(WishlistAggregate wishlist) : base(wishlist)
  {
    Synchronize(wishlist);
  }

  public WishlistEntity(WishlistCreatedEvent @event) : base(@event)
  {
    DisplayName = @event.DisplayName.Value;
  }

  private WishlistEntity() : base()
  {
  }

  public void Synchronize(WishlistAggregate wishlist)
  {
    Update(wishlist);

    DisplayName = wishlist.DisplayName.Value;
    PictureUrl = wishlist.PictureUrl?.Value;
  }

  public void Update(WishlistUpdatedEvent @event)
  {
    base.Update(@event);

    if (@event.DisplayName != null)
    {
      DisplayName = @event.DisplayName.Value;
    }
    if (@event.PictureUrl != null)
    {
      PictureUrl = @event.PictureUrl.Value?.Value;
    }
  }
}
