using Logitar.Wishes.Contracts.Actors;

namespace Logitar.Wishes.Contracts;

public abstract class Metadata
{
  public long Version { get; set; }

  public Actor CreatedBy { get; set; } = new();
  public DateTime CreatedOn { get; set; }

  public Actor UpdatedBy { get; set; } = new();
  public DateTime UpdatedOn { get; set; }
}
