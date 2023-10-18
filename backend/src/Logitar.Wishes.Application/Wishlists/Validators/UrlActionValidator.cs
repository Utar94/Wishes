using FluentValidation;
using Logitar.Wishes.Contracts.Wishlists;
using Logitar.Wishes.Domain.Validators;

namespace Logitar.Wishes.Application.Wishlists.Validators;

internal class UrlActionValidator : AbstractValidator<UrlAction>
{
  public UrlActionValidator()
  {
    RuleFor(x => x.Url).SetValidator(new UrlValidator());

    RuleFor(x => x.Action).IsInEnum();
  }
}
