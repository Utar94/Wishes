using Logitar.Wishes.Domain.ValueObjects;

namespace Logitar.Wishes.Domain.Wishlists;

public record ItemUnit
{
  public DisplayNameUnit DisplayName { get; }
  public SummaryUnit? Summary { get; }
  public UrlUnit? PictureUrl { get; }

  public PriceUnit? Price { get; }
  // TODO(fpion): Rank/Priority?
  public ContentsUnit? Contents { get; }

  // TODO(fpion): Gallery
  // TODO(fpion): Links

  public ItemUnit(DisplayNameUnit displayName, SummaryUnit? summary = null, UrlUnit? pictureUrl = null, PriceUnit? price = null, ContentsUnit? contents = null)
  {
    DisplayName = displayName;
    Summary = summary;
    PictureUrl = pictureUrl;

    Price = price;
    Contents = contents;
  }
}
