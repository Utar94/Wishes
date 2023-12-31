﻿using Logitar.Wishes.Domain.Wishlists;
using Logitar.Wishes.Domain.Wishlists.Events;
using Logitar.Wishes.EntityFrameworkCore.Relational.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Logitar.Wishes.EntityFrameworkCore.Relational.Handlers.Wishlists;

internal class WishlistItemRemovedEventHandler : INotificationHandler<WishlistItemRemovedEvent>
{
  private readonly WishesContext _context;
  private readonly IWishlistRepository _wishlistRepository;

  public WishlistItemRemovedEventHandler(WishesContext context, IWishlistRepository wishlistRepository)
  {
    _context = context;
    _wishlistRepository = wishlistRepository;
  }

  public async Task Handle(WishlistItemRemovedEvent @event, CancellationToken cancellationToken)
  {
    long expectedVersion = @event.Version - 1;

    WishlistEntity? wishlist = await _context.Wishlists
      .Include(x => x.Items)
      .SingleOrDefaultAsync(x => x.AggregateId == @event.AggregateId.Value, cancellationToken);
    if (wishlist == null || wishlist.Version < expectedVersion)
    {
      WishlistId id = new(@event.AggregateId);
      WishlistAggregate aggregate = await _wishlistRepository.LoadAsync(id, expectedVersion, cancellationToken)
        ?? throw new InvalidOperationException($"The wishlist aggregate 'Id={id.Value}' could not be found.");

      if (wishlist == null)
      {
        wishlist = new(aggregate);
        _context.Wishlists.Add(wishlist);
      }
      else
      {
        wishlist.Synchronize(aggregate);
      }
    }

    wishlist.RemoveItem(@event);

    await _context.SaveChangesAsync(cancellationToken);
  }
}
