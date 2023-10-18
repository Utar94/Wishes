using Logitar.Wishes.Contracts.Actors;

namespace Logitar.Wishes.Contracts;

public record AcceptedCommand
{
  public string AggregateId { get; set; } = string.Empty;
  public long Version { get; set; }

  public Actor Actor { get; set; } = new();
  public DateTime Timestamp { get; set; }
}
