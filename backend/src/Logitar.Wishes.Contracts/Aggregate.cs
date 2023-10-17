namespace Logitar.Wishes.Contracts;

public abstract class Aggregate : Metadata
{
  public string Id { get; set; } = string.Empty;

  public override bool Equals(object? obj) => obj is Aggregate aggregate && aggregate.GetType().Equals(GetType()) && aggregate.Id == Id;
  public override int GetHashCode() => HashCode.Combine(GetType(), Id);
  public override string ToString() => $"{GetType()} (Id={Id})";
}
