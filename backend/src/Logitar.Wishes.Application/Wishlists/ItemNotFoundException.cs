using FluentValidation.Results;
using Logitar.Wishes.Domain.Exceptions;
using Logitar.Wishes.Domain.Wishlists;

namespace Logitar.Wishes.Application.Wishlists;

public class ItemNotFoundException : Exception, IFailureException
{
  private const string ErrorMessage = "The specified wishlist item could not be found.";

  public WishlistId WishlistId
  {
    get => WishlistId.Parse((string)Data[nameof(WishlistId)]!);
    private set => Data[nameof(WishlistId)] = value.Value;
  }
  public ItemId ItemId
  {
    get => new((string)Data[nameof(ItemId)]!);
    private set => Data[nameof(ItemId)] = value.Value;
  }
  public string PropertyName
  {
    get => (string)Data[nameof(PropertyName)]!;
    private set => Data[nameof(PropertyName)] = value;
  }

  public ValidationFailure Failure => throw new NotImplementedException();

  public ItemNotFoundException(WishlistAggregate wishlist, ItemId id, string propertyName)
    : base(BuildMessage(wishlist, id, propertyName))
  {
    WishlistId = wishlist.Id;
    ItemId = id;
    PropertyName = propertyName;
  }

  private static string BuildMessage(WishlistAggregate wishlist, ItemId id, string propertyName) => new ExceptionMessageBuilder(ErrorMessage)
    .AddData(nameof(WishlistId), wishlist.Id.Value)
    .AddData(nameof(ItemId), id.Value)
    .AddData(nameof(PropertyName), propertyName)
    .Build();
}
