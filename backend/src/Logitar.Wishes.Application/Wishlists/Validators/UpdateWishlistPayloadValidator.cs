using FluentValidation;
using Logitar.Wishes.Contracts.Wishlists;
using Logitar.Wishes.Domain.Validators;

namespace Logitar.Wishes.Application.Wishlists.Validators;

internal class UpdateWishlistPayloadValidator : AbstractValidator<UpdateWishlistPayload>
{
  public UpdateWishlistPayloadValidator()
  {
    When(x => !string.IsNullOrWhiteSpace(x.DisplayName),
      () => RuleFor(x => x.DisplayName!).SetValidator(new DisplayNameValidator()
    ));

    When(x => !string.IsNullOrWhiteSpace(x.PictureUrl?.Value),
      () => RuleFor(x => x.PictureUrl!.Value!).SetValidator(new UrlValidator())
    );
  }
}
