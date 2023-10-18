using Logitar.Data;
using Logitar.EventSourcing;
using Logitar.Wishes.Application.Actors;
using Logitar.Wishes.Application.Wishlists;
using Logitar.Wishes.Contracts.Actors;
using Logitar.Wishes.Contracts.Search;
using Logitar.Wishes.Contracts.Wishlists;
using Logitar.Wishes.EntityFrameworkCore.Relational.Entities;
using Microsoft.EntityFrameworkCore;

namespace Logitar.Wishes.EntityFrameworkCore.Relational.Queriers;

internal class WishlistQuerier : IWishlistQuerier
{
  private readonly IActorService _actorService;
  private readonly ISqlHelper _sqlHelper;
  private readonly DbSet<WishlistEntity> _wishlists;

  public WishlistQuerier(IActorService actorService, WishesContext context, ISqlHelper sqlHelper)
  {
    _actorService = actorService;
    _wishlists = context.Wishlists;
    _sqlHelper = sqlHelper;
  }

  public async Task<Wishlist?> ReadAsync(string id, CancellationToken cancellationToken)
  {
    WishlistEntity? wishlist = await _wishlists.AsNoTracking()
      .Include(x => x.Items)
      .SingleOrDefaultAsync(x => x.AggregateId == id, cancellationToken);

    return wishlist == null ? null : await MapAsync(wishlist, cancellationToken);
  }

  public async Task<SearchResults<Wishlist>> SearchAsync(SearchWishlistsPayload payload, CancellationToken cancellationToken)
  {
    IQueryBuilder builder = _sqlHelper.QueryFrom(Db.Wishlists.Table).SelectAll(Db.Wishlists.Table);
    _sqlHelper.ApplyTextSearch(builder, payload.Id, Db.Wishlists.AggregateId);
    _sqlHelper.ApplyTextSearch(builder, payload.Search, Db.Wishlists.DisplayName);

    IQueryable<WishlistEntity> query = _wishlists.FromQuery(builder).AsNoTracking();

    long total = await query.LongCountAsync(cancellationToken);

    IOrderedQueryable<WishlistEntity>? ordered = null;
    foreach (WishlistSortOption sort in payload.Sort)
    {
      switch (sort.Field)
      {
        case WishlistSort.DisplayName:
          ordered = (ordered == null)
            ? (sort.IsDescending ? query.OrderByDescending(x => x.DisplayName) : query.OrderBy(x => x.DisplayName))
            : (sort.IsDescending ? ordered.OrderByDescending(x => x.DisplayName) : ordered.OrderBy(x => x.DisplayName));
          break;
        case WishlistSort.ItemCount:
          ordered = (ordered == null)
            ? (sort.IsDescending ? query.OrderByDescending(x => x.ItemCount) : query.OrderBy(x => x.ItemCount))
            : (sort.IsDescending ? ordered.OrderByDescending(x => x.ItemCount) : ordered.OrderBy(x => x.ItemCount));
          break;
        case WishlistSort.UpdatedOn:
          ordered = (ordered == null)
            ? (sort.IsDescending ? query.OrderByDescending(x => x.UpdatedOn) : query.OrderBy(x => x.UpdatedOn))
            : (sort.IsDescending ? ordered.OrderByDescending(x => x.UpdatedOn) : ordered.OrderBy(x => x.UpdatedOn));
          break;
      }
    }
    query = ordered ?? query;

    query = query.ApplyPaging(payload);

    WishlistEntity[] wishlists = await query.ToArrayAsync(cancellationToken);
    IEnumerable<Wishlist> items = await MapAsync(wishlists, cancellationToken);

    return new SearchResults<Wishlist>(items, total);
  }

  private async Task<Wishlist> MapAsync(WishlistEntity wishlist, CancellationToken cancellationToken)
    => (await MapAsync(new[] { wishlist }, cancellationToken)).Single();
  private async Task<IEnumerable<Wishlist>> MapAsync(IEnumerable<WishlistEntity> wishlists, CancellationToken cancellationToken)
  {
    IEnumerable<ActorId> ids = wishlists.SelectMany(wishlist => wishlist.GetActorIds());
    IEnumerable<Actor> actors = await _actorService.FindAsync(ids, cancellationToken);
    Mapper mapper = new(actors);

    return wishlists.Select(mapper.ToWishlist);
  }
}
