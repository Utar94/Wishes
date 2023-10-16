using Logitar.Wishes.Contracts.Wishlists;
using MediatR;

namespace Logitar.Wishes.Application.Wishlists.Queries;

internal class ReadWishlistQueryHandler : IRequestHandler<ReadWishlistQuery, Wishlist?>
{
  private readonly IWishlistQuerier _wishlistQuerier;

  public ReadWishlistQueryHandler(IWishlistQuerier wishlistQuerier)
  {
    _wishlistQuerier = wishlistQuerier;
  }

  public async Task<Wishlist?> Handle(ReadWishlistQuery query, CancellationToken cancellationToken)
  {
    return await _wishlistQuerier.ReadAsync(query.Id, cancellationToken);
  }
}
