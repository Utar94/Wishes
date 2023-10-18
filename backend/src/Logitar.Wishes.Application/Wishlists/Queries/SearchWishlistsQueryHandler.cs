using Logitar.Wishes.Contracts.Search;
using Logitar.Wishes.Contracts.Wishlists;
using MediatR;

namespace Logitar.Wishes.Application.Wishlists.Queries;

internal class SearchWishlistsQueryHandler : IRequestHandler<SearchWishlistsQuery, SearchResults<Wishlist>>
{
  private readonly IWishlistQuerier _wishlistQuerier;

  public SearchWishlistsQueryHandler(IWishlistQuerier wishlistQuerier)
  {
    _wishlistQuerier = wishlistQuerier;
  }

  public async Task<SearchResults<Wishlist>> Handle(SearchWishlistsQuery query, CancellationToken cancellationToken)
  {
    return await _wishlistQuerier.SearchAsync(query.Payload, cancellationToken);
  }
}
