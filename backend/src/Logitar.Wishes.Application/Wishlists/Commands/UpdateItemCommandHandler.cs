using FluentValidation;
using Logitar.Wishes.Application.Exceptions;
using Logitar.Wishes.Application.Wishlists.Validators;
using Logitar.Wishes.Contracts;
using Logitar.Wishes.Contracts.Wishlists;
using Logitar.Wishes.Domain.ValueObjects;
using Logitar.Wishes.Domain.Wishlists;
using MediatR;

namespace Logitar.Wishes.Application.Wishlists.Commands;

internal class UpdateItemCommandHandler : CommandHandler, IRequestHandler<UpdateItemCommand, AcceptedCommand>
{
  private readonly IWishlistRepository _wishlistRepository;

  public UpdateItemCommandHandler(IApplicationContext applicationContext, IWishlistRepository wishlistRepository) : base(applicationContext)
  {
    _wishlistRepository = wishlistRepository;
  }

  public async Task<AcceptedCommand> Handle(UpdateItemCommand command, CancellationToken cancellationToken)
  {
    UpdateItemPayload payload = command.Payload;
    new UpdateItemPayloadValidator().ValidateAndThrow(payload);

    WishlistId wishlistId = WishlistId.Parse(command.WishlistId, nameof(command.WishlistId));
    WishlistAggregate wishlist = await _wishlistRepository.LoadAsync(wishlistId, cancellationToken)
      ?? throw new AggregateNotFoundException<WishlistAggregate>(wishlistId.AggregateId, nameof(command.WishlistId));

    ItemId itemId = ItemId.Parse(command.ItemId, nameof(command.ItemId));
    if (!wishlist.Items.TryGetValue(itemId, out ItemUnit? item))
    {
      throw new ItemNotFoundException(wishlist, itemId, nameof(command.ItemId));
    }

    item = UpdateItem(item, payload);
    wishlist.SetItem(itemId, item, ApplicationContext.ActorId);

    await _wishlistRepository.SaveAsync(wishlist, cancellationToken);

    return Accept(wishlist);
  }

  private static ItemUnit UpdateItem(ItemUnit item, UpdateItemPayload payload)
  {
    DisplayNameUnit displayName = string.IsNullOrWhiteSpace(payload.DisplayName) ? item.DisplayName : new(payload.DisplayName);
    SummaryUnit? summary = payload.Summary == null ? item.Summary : SummaryUnit.TryCreate(payload.Summary.Value);
    UrlUnit? pictureUrl = payload.PictureUrl == null ? item.PictureUrl : UrlUnit.TryCreate(payload.PictureUrl.Value);

    byte rank = payload.Rank ?? item.Rank;
    PriceUnit? price = item.Price;
    if (payload.Price != null)
    {
      price = payload.Price.Value == null ? null : new PriceUnit(payload.Price.Value.Minimum, payload.Price.Value.Maximum);
    }
    ContentsUnit? contents = item.Contents;
    if (payload.Contents != null)
    {
      contents = payload.Contents.Value == null ? null : new ContentsUnit(payload.Contents.Value.Text, payload.Contents.Value.Type);
    }

    HashSet<UrlUnit> gallery = item.Gallery.ToHashSet();
    foreach (UrlAction value in payload.Gallery)
    {
      UrlUnit url = new(value.Url);

      switch (value.Action)
      {
        case CollectionAction.Add:
          gallery.Add(url);
          break;
        case CollectionAction.Remove:
          gallery.Remove(url);
          break;
      }
    }

    HashSet<UrlUnit> links = item.Links.ToHashSet();
    foreach (UrlAction value in payload.Links)
    {
      UrlUnit url = new(value.Url);

      switch (value.Action)
      {
        case CollectionAction.Add:
          links.Add(url);
          break;
        case CollectionAction.Remove:
          links.Remove(url);
          break;
      }
    }

    return new ItemUnit(displayName, summary, pictureUrl, rank, price, contents, gallery, links);
  }
}
