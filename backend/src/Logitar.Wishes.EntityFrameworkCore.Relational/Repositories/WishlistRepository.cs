using Logitar.EventSourcing.EntityFrameworkCore.Relational;
using Logitar.EventSourcing.Infrastructure;
using Logitar.Wishes.Domain.Wishlists;

namespace Logitar.Wishes.EntityFrameworkCore.Relational.Repositories;

internal class WishlistRepository : EventSourcing.EntityFrameworkCore.Relational.AggregateRepository, IWishlistRepository
{
  public WishlistRepository(IEventBus eventBus, EventContext eventContext, IEventSerializer eventSerializer)
    : base(eventBus, eventContext, eventSerializer)
  {
  }

  public async Task<WishlistAggregate?> LoadAsync(WishlistId id, CancellationToken cancellationToken)
    => await LoadAsync(id, version: null, cancellationToken);
  public async Task<WishlistAggregate?> LoadAsync(WishlistId id, long? version, CancellationToken cancellationToken)
    => await LoadAsync<WishlistAggregate>(id.AggregateId, version, cancellationToken);

  public async Task SaveAsync(WishlistAggregate wishlist, CancellationToken cancellationToken)
    => await base.SaveAsync(wishlist, cancellationToken);
}
