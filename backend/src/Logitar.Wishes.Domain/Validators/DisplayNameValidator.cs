using FluentValidation;
using Logitar.Wishes.Domain.Extensions;
using Logitar.Wishes.Domain.ValueObjects;

namespace Logitar.Wishes.Domain.Validators;

public class DisplayNameValidator : AbstractValidator<string>
{
  public DisplayNameValidator(string? propertyName = null)
  {
    RuleFor(x => x).NotEmpty()
      .MaximumLength(DisplayNameUnit.MaximumLength)
      .WithPropertyName(propertyName);
  }
}
