using Logitar.Wishes.Domain.Wishlists;
using Logitar.Wishes.Domain.Wishlists.Events;

namespace Logitar.Wishes.EntityFrameworkCore.Relational.Entities;

internal class WishlistEntity : AggregateEntity
{
  public int WishlistId { get; private set; }

  public string DisplayName { get; private set; } = string.Empty;
  public string? PictureUrl { get; private set; }

  public int ItemCount { get; private set; }
  public List<ItemEntity> Items { get; private set; } = new();

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

  public void RemoveItem(WishlistItemRemovedEvent @event)
  {
    ItemEntity? item = Items.SingleOrDefault(item => item.Id == @event.ItemId.Value);
    if (item != null)
    {
      Items.Remove(item);
    }
  }

  public void SaveItem(WishlistItemSavedEvent @event)
  {
    ItemEntity? item = Items.SingleOrDefault(item => item.Id == @event.ItemId.Value);
    if (item == null)
    {
      item = new(this, @event);
      Items.Add(item);
    }
    else
    {
      item.Update(@event);
    }
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
