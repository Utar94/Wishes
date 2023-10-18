using Logitar.EventSourcing;
using Logitar.Wishes.Contracts;

namespace Logitar.Wishes.Application;

internal abstract class CommandHandler
{
  protected IApplicationContext ApplicationContext { get; }

  protected CommandHandler(IApplicationContext applicationContext)
  {
    ApplicationContext = applicationContext;
  }

  protected AcceptedCommand Accept(AggregateRoot aggregate) => new()
  {
    AggregateId = aggregate.Id.Value,
    Version = aggregate.Version,
    Actor = ApplicationContext.Actor,
    Timestamp = aggregate.UpdatedOn
  };
}
