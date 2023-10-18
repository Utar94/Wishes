using Logitar.EventSourcing;

namespace Logitar.Wishes.EntityFrameworkCore.Relational.Entities;

internal abstract class AggregateEntity : Entity, IMetadata
{
  public string AggregateId { get; private set; } = string.Empty;
  public long Version { get; private set; }

  public string CreatedBy { get; private set; } = string.Empty;
  public DateTime CreatedOn { get; private set; }

  public string UpdatedBy { get; private set; } = string.Empty;
  public DateTime UpdatedOn { get; private set; }

  protected AggregateEntity()
  {
  }
  protected AggregateEntity(AggregateRoot aggregate)
  {
    CreatedBy = aggregate.CreatedBy.Value;
    CreatedOn = aggregate.CreatedOn.ToUniversalTime();

    Update(aggregate);
  }
  protected AggregateEntity(DomainEvent @event)
  {
    CreatedBy = @event.ActorId.Value;
    CreatedOn = @event.OccurredOn.ToUniversalTime();

    Update(@event);
  }

  public virtual IEnumerable<ActorId> GetActorIds() => new ActorId[] { new(CreatedBy), new(UpdatedBy) };

  public void Update(AggregateRoot aggregate)
  {
    Version = aggregate.Version;

    UpdatedBy = aggregate.UpdatedBy.Value;
    UpdatedOn = aggregate.UpdatedOn.ToUniversalTime();
  }
  public void Update(DomainEvent @event)
  {
    Version = @event.Version;

    UpdatedBy = @event.ActorId.Value;
    UpdatedOn = @event.OccurredOn.ToUniversalTime();
  }
}
