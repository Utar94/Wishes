using Logitar.EventSourcing;
using Logitar.Wishes.Domain.Wishlists;
using Logitar.Wishes.Domain.Wishlists.Events;
using System.Text.Json;

namespace Logitar.Wishes.EntityFrameworkCore.Relational.Entities;

internal class ItemEntity : Entity, IMetadata
{
  public int ItemId { get; private set; }

  public WishlistEntity? Wishlist { get; private set; }
  public int WishlistId { get; private set; }

  public string Id { get; private set; } = string.Empty;

  public string DisplayName { get; private set; } = string.Empty;
  public string? Summary { get; private set; }
  public string? PictureUrl { get; private set; }

  public byte Rank { get; private set; }
  public byte RankCategory { get; private set; }
  public double? AveragePrice { get; private set; }
  public double? MinimumPrice { get; private set; }
  public double? MaximumPrice { get; private set; }
  public byte? PriceCategory { get; private set; }
  public string? ContentText { get; private set; }
  public string? ContentType { get; private set; }

  //public string? Gallery { get; private set; }
  //public string? Links { get; private set; }
  public List<string> Gallery { get; private set; } = new();
  public string? GallerySerialized
  {
    get => Gallery.Any() ? JsonSerializer.Serialize(Gallery) : null;
    private set
    {
      if (value == null)
      {
        Gallery.Clear();
      }
      else
      {
        Gallery = JsonSerializer.Deserialize<List<string>>(value) ?? new();
      }
    }
  }
  public List<string> Links { get; private set; } = new();
  public string? LinksSerialized
  {
    get => Links.Any() ? JsonSerializer.Serialize(Links) : null;
    private set
    {
      if (value == null)
      {
        Links.Clear();
      }
      else
      {
        Links = JsonSerializer.Deserialize<List<string>>(value) ?? new();
      }
    }
  }

  public long Version { get; private set; }

  public string CreatedBy { get; private set; } = string.Empty;
  public DateTime CreatedOn { get; private set; }

  public string UpdatedBy { get; private set; } = string.Empty;
  public DateTime UpdatedOn { get; private set; }

  public ItemEntity(WishlistEntity wishlist, WishlistItemSavedEvent @event)
  {
    Wishlist = wishlist;
    WishlistId = wishlist.WishlistId;

    Id = @event.ItemId.Value;

    CreatedBy = @event.ActorId.Value;
    CreatedOn = @event.OccurredOn.ToUniversalTime();

    Update(@event);
  }
  private ItemEntity() : base()
  {
  }

  public IEnumerable<ActorId> GetActorIds()
  {
    List<ActorId> ids = new(capacity: 4)
    {
      new(CreatedBy),
      new(UpdatedBy)
    };

    if (Wishlist != null)
    {
      ids.AddRange(Wishlist.GetActorIds());
    }

    return ids;
  }

  public void Update(WishlistItemSavedEvent @event)
  {
    Version++;

    UpdatedBy = @event.ActorId.Value;
    UpdatedOn = @event.OccurredOn.ToUniversalTime();

    ItemUnit item = @event.Item;

    DisplayName = item.DisplayName.Value;
    Summary = item.Summary?.Value;
    PictureUrl = item.PictureUrl?.Value;

    Rank = item.Rank;
    // TODO(fpion): RankCategory
    AveragePrice = item.Price?.Average;
    MinimumPrice = item.Price?.Minimum;
    MaximumPrice = item.Price?.Maximum;
    // TODO(fpion): PriceCategory
    ContentText = item.Contents?.Text;
    ContentType = item.Contents?.Type;

    Gallery = item.Gallery.Select(url => url.Value).ToList();
    Links = item.Links.Select(url => url.Value).ToList();
  }
}
