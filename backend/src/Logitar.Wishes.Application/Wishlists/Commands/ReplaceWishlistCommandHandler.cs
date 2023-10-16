using FluentValidation;
using Logitar.Wishes.Application.Exceptions;
using Logitar.Wishes.Application.Wishlists.Validators;
using Logitar.Wishes.Contracts;
using Logitar.Wishes.Contracts.Wishlists;
using Logitar.Wishes.Domain.ValueObjects;
using Logitar.Wishes.Domain.Wishlists;
using MediatR;

namespace Logitar.Wishes.Application.Wishlists.Commands;

internal class ReplaceWishlistCommandHandler : CommandHandler, IRequestHandler<ReplaceWishlistCommand, AcceptedCommand>
{
  private readonly IWishlistRepository _wishlistRepository;

  public ReplaceWishlistCommandHandler(IApplicationContext applicationContext, IWishlistRepository wishlistRepository)
    : base(applicationContext)
  {
    _wishlistRepository = wishlistRepository;
  }

  public async Task<AcceptedCommand> Handle(ReplaceWishlistCommand command, CancellationToken cancellationToken)
  {
    ReplaceWishlistPayload payload = command.Payload;
    new ReplaceWishlistPayloadValidator().ValidateAndThrow(payload);

    WishlistId id = WishlistId.Parse(command.Id, nameof(command.Id));
    WishlistAggregate wishlist = await _wishlistRepository.LoadAsync(id, cancellationToken)
      ?? throw new AggregateNotFoundException<WishlistAggregate>(id.AggregateId, nameof(command.Id));

    WishlistAggregate? reference = command.Version.HasValue
      ? await _wishlistRepository.LoadAsync(id, command.Version.Value, cancellationToken)
      : null;

    DisplayNameUnit displayName = new(payload.DisplayName);
    if (reference == null || displayName != reference.DisplayName)
    {
      wishlist.DisplayName = displayName;
    }
    Uri? pictureUrl = string.IsNullOrWhiteSpace(payload.PictureUrl) ? null : new(payload.PictureUrl.Trim());
    if (reference == null || pictureUrl != reference.PictureUrl)
    {
      wishlist.PictureUrl = pictureUrl;
    }

    wishlist.Update(ApplicationContext.ActorId);

    await _wishlistRepository.SaveAsync(wishlist, cancellationToken);

    return Accept(wishlist);
  }
}
