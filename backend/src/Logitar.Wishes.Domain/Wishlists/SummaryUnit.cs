using FluentValidation;

namespace Logitar.Wishes.Domain.Wishlists;

public record SummaryUnit
{
  public const int MaximumLength = 100;

  public string Value { get; }

  public SummaryUnit(string value)
  {
    new SummaryValidator(nameof(Value)).ValidateAndThrow(value);
    Value = value.Trim();
  }

  public static SummaryUnit? TryCreate(string? value) => string.IsNullOrWhiteSpace(value) ? null : new(value.Trim());
}
