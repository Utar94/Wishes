namespace Logitar.Wishes.Contracts.Search;

public record SearchPayload
{
  public TextSearch Id { get; set; } = new();
  public TextSearch Search { get; set; } = new();

  public List<SortOption> Sort { get; set; } = new();

  public long Skip { get; set; }
  public long Limit { get; set; }
}
