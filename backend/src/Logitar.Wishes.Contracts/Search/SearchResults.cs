namespace Logitar.Wishes.Contracts.Search;

public record SearchResults<T>
{
  public List<T> Items { get; set; }
  public long Total { get; set; }

  public SearchResults() : this(Enumerable.Empty<T>())
  {
  }
  public SearchResults(IEnumerable<T> items) : this(items, items.LongCount())
  {
  }
  public SearchResults(long total) : this(Enumerable.Empty<T>(), total)
  {
  }
  public SearchResults(IEnumerable<T> items, long total)
  {
    Items = items.ToList();
    Total = total;
  }
}
