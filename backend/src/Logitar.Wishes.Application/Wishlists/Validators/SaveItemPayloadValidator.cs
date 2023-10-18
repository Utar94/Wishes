using FluentValidation;
using Logitar.Wishes.Contracts.Wishlists;
using Logitar.Wishes.Domain.Validators;
using Logitar.Wishes.Domain.Wishlists;

namespace Logitar.Wishes.Application.Wishlists.Validators;

internal class SaveItemPayloadValidator : AbstractValidator<SaveItemPayload>
{
  public SaveItemPayloadValidator()
  {
    RuleFor(x => x.DisplayName).SetValidator(new DisplayNameValidator());

    When(x => !string.IsNullOrWhiteSpace(x.Summary),
      () => RuleFor(x => x.Summary!).SetValidator(new SummaryValidator())
    );

    When(x => !string.IsNullOrWhiteSpace(x.PictureUrl),
      () => RuleFor(x => x.PictureUrl!).SetValidator(new UrlValidator())
    );

    When(x => x.Price != null,
      () => RuleFor(x => x.Price!).SetValidator(new PricePayloadValidator())
    );

    When(x => x.Contents != null,
      () => RuleFor(x => x.Contents!).SetValidator(new ContentsPayloadValidator())
    );

    RuleForEach(x => x.Gallery).SetValidator(new UrlValidator());

    RuleForEach(x => x.Links).SetValidator(new UrlValidator());
  }
}
