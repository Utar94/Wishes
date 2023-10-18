using FluentValidation;
using Logitar.Wishes.Contracts.Wishlists;

namespace Logitar.Wishes.Application.Wishlists.Validators;

internal class PricePayloadValidator : AbstractValidator<PricePayload>
{
  public PricePayloadValidator()
  {
    RuleFor(x => x.Minimum).GreaterThan(0);

    RuleFor(x => x.Maximum).GreaterThanOrEqualTo(x => x.Minimum);
  }
}
