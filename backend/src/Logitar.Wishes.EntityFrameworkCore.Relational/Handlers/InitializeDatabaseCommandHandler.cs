using Logitar.Wishes.Infrastructure.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Logitar.Wishes.EntityFrameworkCore.Relational.Handlers;

internal class InitializeDatabaseCommandHandler : INotificationHandler<InitializeDatabaseCommand>
{
  private readonly IConfiguration _configuration;
  private readonly WishesContext _context;

  public InitializeDatabaseCommandHandler(IConfiguration configuration, WishesContext context)
  {
    _configuration = configuration;
    _context = context;
  }

  public async Task Handle(InitializeDatabaseCommand command, CancellationToken cancellationToken)
  {
    if (_configuration.GetValue<bool>("EnableMigrations"))
    {
      await _context.Database.MigrateAsync(cancellationToken);
    }
  }
}
