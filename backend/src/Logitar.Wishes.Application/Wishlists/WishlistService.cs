using Logitar.Wishes.Application.Wishlists.Commands;
using Logitar.Wishes.Application.Wishlists.Queries;
using Logitar.Wishes.Contracts;
using Logitar.Wishes.Contracts.Search;
using Logitar.Wishes.Contracts.Wishlists;
using MediatR;

namespace Logitar.Wishes.Application.Wishlists;

internal class WishlistService : IWishlistService
{
  private readonly IMediator _mediator;

  public WishlistService(IMediator mediator)
  {
    _mediator = mediator;
  }

  public async Task<AcceptedCommand> CreateAsync(CreateWishlistPayload payload, CancellationToken cancellationToken)
  {
    return await _mediator.Send(new CreateWishlistCommand(payload), cancellationToken);
  }

  public async Task<AcceptedCommand> DeleteAsync(string id, CancellationToken cancellationToken)
  {
    return await _mediator.Send(new DeleteWishlistCommand(id), cancellationToken);
  }

  public async Task<Wishlist?> ReadAsync(string id, CancellationToken cancellationToken)
  {
    return await _mediator.Send(new ReadWishlistQuery(id), cancellationToken);
  }

  public async Task<AcceptedCommand> ReplaceAsync(string id, ReplaceWishlistPayload payload, long? version, CancellationToken cancellationToken)
  {
    return await _mediator.Send(new ReplaceWishlistCommand(id, payload, version), cancellationToken);
  }

  public async Task<SearchResults<Wishlist>> SearchAsync(SearchWishlistsPayload payload, CancellationToken cancellationToken)
  {
    return await _mediator.Send(new SearchWishlistsQuery(payload), cancellationToken);
  }

  public async Task<AcceptedCommand> UpdateAsync(string id, UpdateWishlistPayload payload, CancellationToken cancellationToken)
  {
    return await _mediator.Send(new UpdateWishlistCommand(id, payload), cancellationToken);
  }
}
