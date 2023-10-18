using Logitar.Wishes.Application.Exceptions;
using Logitar.Wishes.Contracts;
using Logitar.Wishes.Domain.Wishlists;
using MediatR;

namespace Logitar.Wishes.Application.Wishlists.Commands;

internal class RemoveItemCommandHandler : CommandHandler, IRequestHandler<RemoveItemCommand, AcceptedCommand>
{
  private readonly IWishlistRepository _wishlistRepository;

  public RemoveItemCommandHandler(IApplicationContext applicationContext, IWishlistRepository wishlistRepository) : base(applicationContext)
  {
    _wishlistRepository = wishlistRepository;
  }

  public async Task<AcceptedCommand> Handle(RemoveItemCommand command, CancellationToken cancellationToken)
  {
    WishlistId wishlistId = WishlistId.Parse(command.WishlistId, nameof(command.WishlistId));
    WishlistAggregate wishlist = await _wishlistRepository.LoadAsync(wishlistId, cancellationToken)
      ?? throw new AggregateNotFoundException<WishlistAggregate>(wishlistId.AggregateId, nameof(command.WishlistId));

    ItemId itemId = ItemId.Parse(command.ItemId, nameof(command.ItemId));
    wishlist.RemoveItem(itemId, ApplicationContext.ActorId);

    await _wishlistRepository.SaveAsync(wishlist, cancellationToken);

    return Accept(wishlist);
  }
}
