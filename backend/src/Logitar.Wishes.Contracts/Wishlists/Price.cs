namespace Logitar.Wishes.Contracts.Wishlists;

public record Price
{
  public double Average { get; set; }
  public double Minimum { get; set; }
  public double Maximum { get; set; }
  public byte Category { get; set; }
}
