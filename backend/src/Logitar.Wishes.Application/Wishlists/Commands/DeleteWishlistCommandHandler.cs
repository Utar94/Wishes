using Logitar.Wishes.Application.Exceptions;
using Logitar.Wishes.Contracts;
using Logitar.Wishes.Domain.Wishlists;
using MediatR;

namespace Logitar.Wishes.Application.Wishlists.Commands;

internal class DeleteWishlistCommandHandler : CommandHandler, IRequestHandler<DeleteWishlistCommand, AcceptedCommand>
{
  private readonly IWishlistRepository _wishlistRepository;

  public DeleteWishlistCommandHandler(IApplicationContext applicationContext, IWishlistRepository wishlistRepository)
    : base(applicationContext)
  {
    _wishlistRepository = wishlistRepository;
  }

  public async Task<AcceptedCommand> Handle(DeleteWishlistCommand command, CancellationToken cancellationToken)
  {
    WishlistId id = WishlistId.Parse(command.Id, nameof(command.Id));
    WishlistAggregate wishlist = await _wishlistRepository.LoadAsync(id, cancellationToken)
      ?? throw new AggregateNotFoundException<WishlistAggregate>(id.AggregateId, nameof(command.Id));

    wishlist.Delete(ApplicationContext.ActorId);

    await _wishlistRepository.SaveAsync(wishlist, cancellationToken);

    return Accept(wishlist);
  }
}
