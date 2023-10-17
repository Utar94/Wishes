using Logitar.Wishes.Application.Exceptions;
using Logitar.Wishes.Contracts;
using Logitar.Wishes.Contracts.Wishlists;
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
    //new UpdateItemPayloadValidator().ValidateAndThrow(payload); // TODO(fpion): implement

    WishlistId wishlistId = WishlistId.Parse(command.WishlistId, nameof(command.WishlistId));
    WishlistAggregate wishlist = await _wishlistRepository.LoadAsync(wishlistId, cancellationToken)
      ?? throw new AggregateNotFoundException<WishlistAggregate>(wishlistId.AggregateId, nameof(command.WishlistId));

    ItemId itemId = ItemId.Parse(command.ItemId, nameof(command.ItemId));
    if (!wishlist.Items.TryGetValue(itemId, out ItemUnit? item))
    {
      throw new NotImplementedException(); // TODO(fpion): implement
    }

    item = UpdateItem(item, payload);
    wishlist.SetItem(itemId, item, ApplicationContext.ActorId);

    await _wishlistRepository.SaveAsync(wishlist, cancellationToken);

    return Accept(wishlist);
  }

  private static ItemUnit UpdateItem(ItemUnit item, UpdateItemPayload payload)
  {
    throw new NotImplementedException(); // TODO(fpion): implement
    //DisplayNameUnit displayName = new(payload.DisplayName);
    //SummaryUnit? summary = SummaryUnit.TryCreate(payload.Summary);
    //UrlUnit? pictureUrl = UrlUnit.TryCreate(payload.PictureUrl);

    //PriceUnit? price = payload.Price == null ? null : new(payload.Price.Minimum, payload.Price.Maximum);
    //ContentsUnit? contents = payload.Contents == null ? null : new(payload.Contents.Text, payload.Contents.Type);

    //return new ItemUnit(displayName, summary, pictureUrl, price, contents);
  }
}
