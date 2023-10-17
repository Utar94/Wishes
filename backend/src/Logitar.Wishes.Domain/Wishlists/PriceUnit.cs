namespace Logitar.Wishes.Domain.Wishlists;

public record PriceUnit
{
  public double Minimum { get; }
  public double Maximum { get; }

  public double Average => (Minimum + Maximum) / 2.0;

  public PriceUnit(double minimum, double maximum)
  {
    if (minimum <= 0)
    {
      throw new ArgumentOutOfRangeException(nameof(minimum), "The value must be superior to 0.");
    }
    if (maximum <= 0)
    {
      throw new ArgumentOutOfRangeException(nameof(maximum), "The value must be superior to 0.");
    }
    if (minimum > maximum)
    {
      throw new ArgumentOutOfRangeException(nameof(maximum), "The maximum price must be superior or equal to the minimum price.");
    }

    Minimum = minimum;
    Maximum = maximum;
  }
}
