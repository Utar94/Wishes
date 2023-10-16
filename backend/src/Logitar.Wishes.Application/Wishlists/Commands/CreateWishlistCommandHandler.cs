using FluentValidation;
using Logitar.Wishes.Application.Exceptions;
using Logitar.Wishes.Application.Extensions;
using Logitar.Wishes.Application.Wishlists.Validators;
using Logitar.Wishes.Contracts;
using Logitar.Wishes.Contracts.Wishlists;
using Logitar.Wishes.Domain.ValueObjects;
using Logitar.Wishes.Domain.Wishlists;
using MediatR;

namespace Logitar.Wishes.Application.Wishlists.Commands;

internal class CreateWishlistCommandHandler : CommandHandler, IRequestHandler<CreateWishlistCommand, AcceptedCommand>
{
  private readonly IWishlistRepository _wishlistRepository;

  public CreateWishlistCommandHandler(IApplicationContext applicationContext, IWishlistRepository wishlistRepository)
    : base(applicationContext)
  {
    _wishlistRepository = wishlistRepository;
  }

  public async Task<AcceptedCommand> Handle(CreateWishlistCommand command, CancellationToken cancellationToken)
  {
    CreateWishlistPayload payload = command.Payload;
    new CreateWishlistPayloadValidator().ValidateAndThrow(payload);

    WishlistId? id = await ResolveIdAsync(payload, cancellationToken);

    DisplayNameUnit displayName = new(payload.DisplayName);
    WishlistAggregate wishlist = new(displayName, ApplicationContext.ActorId, id);

    if (!string.IsNullOrWhiteSpace(payload.PictureUrl))
    {
      wishlist.PictureUrl = new Uri(payload.PictureUrl.Trim());
    }

    wishlist.Update(ApplicationContext.ActorId);

    await _wishlistRepository.SaveAsync(wishlist, cancellationToken);

    return Accept(wishlist);
  }

  private async Task<WishlistId?> ResolveIdAsync(CreateWishlistPayload payload, CancellationToken cancellationToken)
  {
    WishlistId? id;

    if (string.IsNullOrWhiteSpace(payload.Id))
    {
      id = WishlistId.Parse(payload.DisplayName.Slugify());

      if (await _wishlistRepository.LoadAsync(id, cancellationToken) != null)
      {
        id = null;
      }
    }
    else
    {
      id = WishlistId.Parse(payload.Id);

      if (await _wishlistRepository.LoadAsync(id, cancellationToken) != null)
      {
        throw new IdentifierAlreadyUsedException<WishlistAggregate>(id.AggregateId, nameof(payload.Id));
      }
    }

    return id;
  }
}
