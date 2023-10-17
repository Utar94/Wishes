using FluentValidation;
using Logitar.EventSourcing;
using Logitar.Wishes.Application.Exceptions;
using Logitar.Wishes.Application.Extensions;
using Logitar.Wishes.Application.Wishlists.Validators;
using Logitar.Wishes.Contracts;
using Logitar.Wishes.Contracts.Wishlists;
using Logitar.Wishes.Domain.ValueObjects;
using Logitar.Wishes.Domain.Wishlists;
using MediatR;

namespace Logitar.Wishes.Application.Wishlists.Commands;

internal class SaveItemCommandHandler : CommandHandler, IRequestHandler<SaveItemCommand, AcceptedCommand>
{
  private readonly IWishlistRepository _wishlistRepository;

  public SaveItemCommandHandler(IApplicationContext applicationContext, IWishlistRepository wishlistRepository) : base(applicationContext)
  {
    _wishlistRepository = wishlistRepository;
  }

  public async Task<AcceptedCommand> Handle(SaveItemCommand command, CancellationToken cancellationToken)
  {
    SaveItemPayload payload = command.Payload;
    new SaveItemPayloadValidator().ValidateAndThrow(payload);

    WishlistId wishlistId = WishlistId.Parse(command.WishlistId, nameof(command.WishlistId));
    WishlistAggregate wishlist = await _wishlistRepository.LoadAsync(wishlistId, cancellationToken)
      ?? throw new AggregateNotFoundException<WishlistAggregate>(wishlistId.AggregateId, nameof(command.WishlistId));

    ItemId itemId = ResolveItemId(payload, wishlist, command.ItemId, nameof(command.ItemId));
    ItemUnit item = CreateItem(payload);
    wishlist.SetItem(itemId, item, ApplicationContext.ActorId);

    await _wishlistRepository.SaveAsync(wishlist, cancellationToken);

    return Accept(wishlist);
  }

  private static ItemUnit CreateItem(SaveItemPayload payload)
  {
    DisplayNameUnit displayName = new(payload.DisplayName);
    SummaryUnit? summary = SummaryUnit.TryCreate(payload.Summary);
    UrlUnit? pictureUrl = UrlUnit.TryCreate(payload.PictureUrl);

    byte rank = payload.Rank;
    PriceUnit? price = payload.Price == null ? null : new(payload.Price.Minimum, payload.Price.Maximum);
    ContentsUnit? contents = payload.Contents == null ? null : new(payload.Contents.Text, payload.Contents.Type);

    HashSet<UrlUnit> gallery = payload.Gallery.Select(value => new UrlUnit(value)).ToHashSet();
    HashSet<UrlUnit> links = payload.Links.Select(value => new UrlUnit(value)).ToHashSet();

    return new ItemUnit(displayName, summary, pictureUrl, rank, price, contents, gallery, links);
  }

  private static ItemId ResolveItemId(SaveItemPayload payload, WishlistAggregate wishlist, string? itemId, string propertyName)
  {
    if (!string.IsNullOrWhiteSpace(itemId))
    {
      return ItemId.Parse(itemId, propertyName);
    }

    string value = payload.DisplayName.Slugify();
    if (value.Length > ItemId.MaximumLength)
    {
      value = value[..ItemId.MaximumLength].Trim('-');
    }

    ItemId id = new(value);
    if (!wishlist.Items.ContainsKey(id))
    {
      return id;
    }

    return new ItemId(AggregateId.NewId().Value);
  }
}
