using FluentValidation;
using Logitar.Wishes.Domain.Validators;

namespace Logitar.Wishes.Domain.ValueObjects;

public record DisplayNameUnit
{
  public const int MaximumLength = 50;

  public string Value { get; }

  public DisplayNameUnit(string value)
  {
    new DisplayNameValidator(nameof(Value)).ValidateAndThrow(value);
    Value = value.Trim();
  }
}
