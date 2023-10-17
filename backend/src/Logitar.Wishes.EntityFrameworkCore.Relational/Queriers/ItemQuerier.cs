using Logitar.EventSourcing;
using Logitar.Wishes.Application.Actors;
using Logitar.Wishes.Application.Wishlists;
using Logitar.Wishes.Contracts.Actors;
using Logitar.Wishes.Contracts.Wishlists;
using Logitar.Wishes.Domain.Wishlists;
using Logitar.Wishes.EntityFrameworkCore.Relational.Entities;
using Microsoft.EntityFrameworkCore;

namespace Logitar.Wishes.EntityFrameworkCore.Relational.Queriers;

internal class ItemQuerier : IItemQuerier
{
  private readonly IActorService _actorService;
  private readonly DbSet<ItemEntity> _items;

  public ItemQuerier(IActorService actorService, WishesContext context)
  {
    _actorService = actorService;
    _items = context.Items;
  }

  public async Task<Item?> ReadAsync(WishlistId wishlistId, ItemId itemId, CancellationToken cancellationToken)
  {
    ItemEntity? item = await _items.AsNoTracking()
      .Include(x => x.Wishlist)
      .SingleOrDefaultAsync(x => x.Wishlist!.AggregateId == wishlistId.Value && x.Id == itemId.Value, cancellationToken);

    return item == null ? null : await MapAsync(item, cancellationToken);
  }

  private async Task<Item> MapAsync(ItemEntity item, CancellationToken cancellationToken)
    => (await MapAsync(new[] { item }, cancellationToken)).Single();
  private async Task<IEnumerable<Item>> MapAsync(IEnumerable<ItemEntity> items, CancellationToken cancellationToken)
  {
    IEnumerable<ActorId> ids = items.SelectMany(item => item.GetActorIds());
    IEnumerable<Actor> actors = await _actorService.FindAsync(ids, cancellationToken);
    Mapper mapper = new(actors);

    return items.Select(mapper.ToItem);
  }
}
