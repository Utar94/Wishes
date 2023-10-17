namespace Logitar.Wishes.Contracts.Wishlists;

public record UpdateItemPayload
{
  public string? Title { get; set; }
  public Modification<string>? Summary { get; set; }
  public Modification<string>? PictureUrl { get; set; }

  public Modification<PricePayload>? Price { get; set; }
  // TODO(fpion): Rank/Priority?
  public Modification<ContentsPayload>? Contents { get; set; }

  //public List<string> Gallery { get; set; } = new(); // TODO(fpion): implement
  //public List<string> Links { get; set; } = new(); // TODO(fpion): implement
}
