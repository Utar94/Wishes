using FluentValidation;
using Logitar.Wishes.Contracts.Wishlists;
using Logitar.Wishes.Domain.Validators;

namespace Logitar.Wishes.Application.Wishlists.Validators;

internal class ReplaceWishlistPayloadValidator : AbstractValidator<ReplaceWishlistPayload>
{
  public ReplaceWishlistPayloadValidator()
  {
    RuleFor(x => x.DisplayName).SetValidator(new DisplayNameValidator());

    When(x => !string.IsNullOrWhiteSpace(x.PictureUrl),
      () => RuleFor(x => x.PictureUrl!).SetValidator(new UrlValidator())
    );
  }
}
