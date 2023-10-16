using FluentValidation;
using Logitar.Wishes.Application.Exceptions;
using Logitar.Wishes.Application.Wishlists.Validators;
using Logitar.Wishes.Contracts;
using Logitar.Wishes.Contracts.Wishlists;
using Logitar.Wishes.Domain.ValueObjects;
using Logitar.Wishes.Domain.Wishlists;
using MediatR;

namespace Logitar.Wishes.Application.Wishlists.Commands;

internal class UpdateWishlistCommandHandler : CommandHandler, IRequestHandler<UpdateWishlistCommand, AcceptedCommand>
{
  private readonly IWishlistRepository _wishlistRepository;

  public UpdateWishlistCommandHandler(IApplicationContext applicationContext, IWishlistRepository wishlistRepository)
    : base(applicationContext)
  {
    _wishlistRepository = wishlistRepository;
  }

  public async Task<AcceptedCommand> Handle(UpdateWishlistCommand command, CancellationToken cancellationToken)
  {
    UpdateWishlistPayload payload = command.Payload;
    new UpdateWishlistPayloadValidator().ValidateAndThrow(payload);

    WishlistId id = WishlistId.Parse(command.Id, nameof(command.Id));
    WishlistAggregate wishlist = await _wishlistRepository.LoadAsync(id, cancellationToken)
      ?? throw new AggregateNotFoundException<WishlistAggregate>(id.AggregateId, nameof(command.Id));

    if (!string.IsNullOrWhiteSpace(payload.DisplayName))
    {
      wishlist.DisplayName = new DisplayNameUnit(payload.DisplayName);
    }
    if (payload.PictureUrl != null)
    {
      wishlist.PictureUrl = string.IsNullOrWhiteSpace(payload.PictureUrl.Value) ? null : new Uri(payload.PictureUrl.Value.Trim());
    }

    wishlist.Update(ApplicationContext.ActorId);

    await _wishlistRepository.SaveAsync(wishlist, cancellationToken);

    return Accept(wishlist);
  }
}
