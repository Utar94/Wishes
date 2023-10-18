using FluentValidation;
using Logitar.Wishes.Domain.Extensions;

namespace Logitar.Wishes.Domain.Wishlists;

public class SummaryValidator : AbstractValidator<string>
{
  public SummaryValidator(string? propertyName = null)
  {
    RuleFor(x => x).NotEmpty()
      .MaximumLength(SummaryUnit.MaximumLength)
      .WithPropertyName(propertyName);
  }
}
