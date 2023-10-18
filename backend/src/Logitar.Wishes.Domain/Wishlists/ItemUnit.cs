using Logitar.Wishes.Domain.ValueObjects;

namespace Logitar.Wishes.Domain.Wishlists;

public record ItemUnit
{
  public DisplayNameUnit DisplayName { get; }
  public SummaryUnit? Summary { get; }
  public UrlUnit? PictureUrl { get; }

  public byte Rank { get; }
  public PriceUnit? Price { get; }
  public ContentsUnit? Contents { get; }

  public IEnumerable<UrlUnit> Gallery { get; }
  public IEnumerable<UrlUnit> Links { get; }

  public ItemUnit(DisplayNameUnit displayName, SummaryUnit? summary = null, UrlUnit? pictureUrl = null, byte rank = 0,
    PriceUnit? price = null, ContentsUnit? contents = null, IEnumerable<UrlUnit>? gallery = null, IEnumerable<UrlUnit>? links = null)
  {
    DisplayName = displayName;
    Summary = summary;
    PictureUrl = pictureUrl;

    Rank = rank;
    Price = price;
    Contents = contents;

    Gallery = gallery ?? Enumerable.Empty<UrlUnit>();
    Links = links ?? Enumerable.Empty<UrlUnit>();
  }
}
