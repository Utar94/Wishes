using Logitar.Data;
using Logitar.EventSourcing;
using Logitar.Wishes.Application.Actors;
using Logitar.Wishes.Application.Wishlists;
using Logitar.Wishes.Contracts.Actors;
using Logitar.Wishes.Contracts.Search;
using Logitar.Wishes.Contracts.Wishlists;
using Logitar.Wishes.Domain.Wishlists;
using Logitar.Wishes.EntityFrameworkCore.Relational.Entities;
using Microsoft.EntityFrameworkCore;

namespace Logitar.Wishes.EntityFrameworkCore.Relational.Queriers;

internal class ItemQuerier : IItemQuerier
{
  private readonly IActorService _actorService;
  private readonly DbSet<ItemEntity> _items;
  private readonly ISqlHelper _sqlHelper;

  public ItemQuerier(IActorService actorService, WishesContext context, ISqlHelper sqlHelper)
  {
    _actorService = actorService;
    _items = context.Items;
    _sqlHelper = sqlHelper;
  }

  public async Task<Item?> ReadAsync(WishlistId wishlistId, ItemId itemId, CancellationToken cancellationToken)
  {
    ItemEntity? item = await _items.AsNoTracking()
      .Include(x => x.Wishlist)
      .SingleOrDefaultAsync(x => x.Wishlist!.AggregateId == wishlistId.Value && x.Id == itemId.Value, cancellationToken);

    return item == null ? null : await MapAsync(item, cancellationToken);
  }

  public async Task<SearchResults<Item>> SearchAsync(SearchItemsPayload payload, CancellationToken cancellationToken)
  {
    WishlistId wishlistId = WishlistId.Parse(payload.WishlistId, nameof(payload.WishlistId));

    IQueryBuilder builder = _sqlHelper.QueryFrom(Db.Items.Table)
      .Join(Db.Wishlists.WishlistId, Db.Items.WishlistId)
      .Where(Db.Wishlists.AggregateId, Operators.IsEqualTo(wishlistId.Value))
      .SelectAll(Db.Items.Table);
    _sqlHelper.ApplyTextSearch(builder, payload.Id, Db.Items.Id);
    _sqlHelper.ApplyTextSearch(builder, payload.Search, Db.Items.DisplayName, Db.Items.Summary);

    IQueryable<ItemEntity> query = _items.FromQuery(builder).AsNoTracking()
      .Include(x => x.Wishlist);

    long total = await query.LongCountAsync(cancellationToken);

    IOrderedQueryable<ItemEntity>? ordered = null;
    foreach (ItemSortOption sort in payload.Sort)
    {
      switch (sort.Field)
      {
        case ItemSort.AveragePrice:
          ordered = (ordered == null)
            ? (sort.IsDescending ? query.OrderByDescending(x => x.AveragePrice) : query.OrderBy(x => x.AveragePrice))
            : (sort.IsDescending ? ordered.OrderByDescending(x => x.AveragePrice) : ordered.OrderBy(x => x.AveragePrice));
          break;
        case ItemSort.DisplayName:
          ordered = (ordered == null)
            ? (sort.IsDescending ? query.OrderByDescending(x => x.DisplayName) : query.OrderBy(x => x.DisplayName))
            : (sort.IsDescending ? ordered.OrderByDescending(x => x.DisplayName) : ordered.OrderBy(x => x.DisplayName));
          break;
        case ItemSort.Rank:
          ordered = (ordered == null)
            ? (sort.IsDescending ? query.OrderByDescending(x => x.Rank) : query.OrderBy(x => x.Rank))
            : (sort.IsDescending ? ordered.OrderByDescending(x => x.Rank) : ordered.OrderBy(x => x.Rank));
          break;
        case ItemSort.UpdatedOn:
          ordered = (ordered == null)
            ? (sort.IsDescending ? query.OrderByDescending(x => x.UpdatedOn) : query.OrderBy(x => x.UpdatedOn))
            : (sort.IsDescending ? ordered.OrderByDescending(x => x.UpdatedOn) : ordered.OrderBy(x => x.UpdatedOn));
          break;
      }
    }
    query = ordered ?? query;

    query = query.ApplyPaging(payload);

    ItemEntity[] entities = await query.ToArrayAsync(cancellationToken);
    IEnumerable<Item> items = await MapAsync(entities, cancellationToken);

    return new SearchResults<Item>(items, total);
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
