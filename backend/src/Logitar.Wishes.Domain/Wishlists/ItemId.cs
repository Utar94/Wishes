using FluentValidation;

namespace Logitar.Wishes.Domain.Wishlists;

public record ItemId
{
  public const int MaximumLength = 32;

  public string Value { get; }

  public ItemId(string value)
  {
    new ItemIdValidator(nameof(Value)).ValidateAndThrow(value);

    Value = value.Trim();
  }

  public static ItemId Parse(string value, string? propertyName = null)
  {
    new ItemIdValidator(propertyName).ValidateAndThrow(value);

    return new ItemId(value);
  }
}
