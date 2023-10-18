using Logitar.EventSourcing.EntityFrameworkCore.Relational;
using Logitar.Wishes.Infrastructure.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Logitar.Wishes.EntityFrameworkCore.Relational.Handlers;

internal class InitializeDatabaseCommandHandler : INotificationHandler<InitializeDatabaseCommand>
{
  private readonly IConfiguration _configuration;
  private readonly EventContext _eventContext;
  private readonly WishesContext _wishesContext;

  public InitializeDatabaseCommandHandler(IConfiguration configuration, EventContext eventContext, WishesContext wishesContext)
  {
    _configuration = configuration;
    _eventContext = eventContext;
    _wishesContext = wishesContext;
  }

  public async Task Handle(InitializeDatabaseCommand command, CancellationToken cancellationToken)
  {
    if (_configuration.GetValue<bool>("EnableMigrations"))
    {
      await _eventContext.Database.MigrateAsync(cancellationToken);
      await _wishesContext.Database.MigrateAsync(cancellationToken);
    }
  }
}
