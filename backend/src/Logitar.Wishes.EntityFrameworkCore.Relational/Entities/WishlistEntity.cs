using Logitar.EventSourcing;
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

  public override IEnumerable<ActorId> GetActorIds() => GetActorIds(includeItems: true);
  public IEnumerable<ActorId> GetActorIds(bool includeItems)
  {
    List<ActorId> actorIds = new(capacity: 2 + (2 * Items.Count))
    {
      new(CreatedBy),
      new(UpdatedBy)
    };

    if (includeItems)
    {
      actorIds.AddRange(Items.SelectMany(item => item.GetActorIds(includeWishlist: false)));
    }

    return actorIds;
  }

  public void RemoveItem(WishlistItemRemovedEvent @event)
  {
    ItemEntity? item = Items.SingleOrDefault(item => item.Id == @event.ItemId.Value);
    if (item != null)
    {
      Items.Remove(item);
      ItemCount = Items.Count;

      RecomputeCategories();
    }
  }

  public void SaveItem(WishlistItemSavedEvent @event)
  {
    ItemEntity? item = Items.SingleOrDefault(item => item.Id == @event.ItemId.Value);
    if (item == null)
    {
      item = new(this, @event);

      Items.Add(item);
      ItemCount = Items.Count;
    }
    else
    {
      item.Update(@event);
    }

    RecomputeCategories();
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

  private void RecomputeCategories()
  {
    RecomputeRankCategories();
    RecomputePriceCategories();
  }
  private void RecomputeRankCategories()
  {
    ItemEntity[] items = Items.OrderByDescending(item => item.Rank).ToArray();
    for (int i = 0; i < items.Length; i++)
    {
      items[i].RankCategory = (byte)Math.Ceiling((i + 1) * 3 / (double)items.Length);
    }
  }
  private void RecomputePriceCategories()
  {
    ItemEntity[] items = Items.Where(item => item.AveragePrice.HasValue).OrderBy(item => item.AveragePrice!.Value).ToArray();
    for (int i = 0; i < items.Length; i++)
    {
      items[i].PriceCategory = (byte)Math.Ceiling((i + 1) * 3 / (double)items.Length);
    }
  }
}
