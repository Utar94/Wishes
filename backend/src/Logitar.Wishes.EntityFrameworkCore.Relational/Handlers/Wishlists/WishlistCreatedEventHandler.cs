using Logitar.Wishes.Domain.Wishlists.Events;
using Logitar.Wishes.EntityFrameworkCore.Relational.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Logitar.Wishes.EntityFrameworkCore.Relational.Handlers.Wishlists;

internal class WishlistCreatedEventHandler : INotificationHandler<WishlistCreatedEvent>
{
  private readonly WishesContext _context;

  public WishlistCreatedEventHandler(WishesContext context)
  {
    _context = context;
  }

  public async Task Handle(WishlistCreatedEvent @event, CancellationToken cancellationToken)
  {
    WishlistEntity? wishlist = await _context.Wishlists.AsNoTracking()
      .SingleOrDefaultAsync(x => x.AggregateId == @event.AggregateId.Value, cancellationToken);
    if (wishlist == null)
    {
      wishlist = new(@event);
      _context.Wishlists.Add(wishlist);

      await _context.SaveChangesAsync(cancellationToken);
    }
  }
}
