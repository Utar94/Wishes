using FluentValidation;
using Logitar.EventSourcing;
using Logitar.Wishes.Domain.Validators;

namespace Logitar.Wishes.Domain.Wishlists;

public record WishlistId
{
  public AggregateId AggregateId { get; }
  public string Value => AggregateId.Value;

  public WishlistId(AggregateId aggregateId)
  {
    AggregateId = aggregateId;
  }
  private WishlistId(string value) : this(new AggregateId(value))
  {
  }

  public static WishlistId Parse(string value, string? propertyName = null)
  {
    new AggregateIdValidator(propertyName).ValidateAndThrow(value);

    return new WishlistId(value);
  }
}
