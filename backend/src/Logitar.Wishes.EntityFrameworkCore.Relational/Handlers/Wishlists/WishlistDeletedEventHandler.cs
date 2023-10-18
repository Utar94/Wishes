using Logitar.Wishes.Domain.Wishlists.Events;
using Logitar.Wishes.EntityFrameworkCore.Relational.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Logitar.Wishes.EntityFrameworkCore.Relational.Handlers.Wishlists;

internal class WishlistDeletedEventHandler : INotificationHandler<WishlistDeletedEvent>
{
  private readonly WishesContext _context;

  public WishlistDeletedEventHandler(WishesContext context)
  {
    _context = context;
  }

  public async Task Handle(WishlistDeletedEvent @event, CancellationToken cancellationToken)
  {
    WishlistEntity? wishlist = await _context.Wishlists
      .SingleOrDefaultAsync(x => x.AggregateId == @event.AggregateId.Value, cancellationToken);
    if (wishlist != null)
    {
      _context.Wishlists.Remove(wishlist);

      await _context.SaveChangesAsync(cancellationToken);
    }
  }
}
