namespace Logitar.Wishes.Contracts.Wishlists;

public record PricePayload
{
  public double Minimum { get; set; }
  public double Maximum { get; set; }
}
