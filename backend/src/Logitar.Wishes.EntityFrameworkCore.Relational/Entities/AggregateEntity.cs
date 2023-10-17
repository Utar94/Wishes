using Logitar.EventSourcing;

namespace Logitar.Wishes.EntityFrameworkCore.Relational.Entities;

internal abstract class AggregateEntity : Entity
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
    AggregateId = aggregate.Id.Value;

    CreatedBy = aggregate.CreatedBy.Value;
    CreatedOn = aggregate.CreatedOn.ToUniversalTime();

    Update(aggregate);
  }

  protected AggregateEntity(DomainEvent @event)
  {
    AggregateId = @event.AggregateId.Value;

    CreatedBy = @event.ActorId.Value;
    CreatedOn = @event.OccurredOn.ToUniversalTime();

    Update(@event);
  }

  public IEnumerable<ActorId> GetActorIds() => new ActorId[] { new(CreatedBy), new(UpdatedBy) };

  protected void Update(AggregateRoot aggregate)
  {
    Version = aggregate.Version;

    UpdatedBy = aggregate.UpdatedBy.Value;
    UpdatedOn = aggregate.UpdatedOn.ToUniversalTime();
  }

  protected void Update(DomainEvent @event)
  {
    Version = @event.Version;

    UpdatedBy = @event.ActorId.Value;
    UpdatedOn = @event.OccurredOn.ToUniversalTime();
  }
}
