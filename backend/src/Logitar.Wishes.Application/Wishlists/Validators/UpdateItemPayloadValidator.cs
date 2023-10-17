using FluentValidation;
using Logitar.Wishes.Contracts.Wishlists;
using Logitar.Wishes.Domain.Validators;
using Logitar.Wishes.Domain.Wishlists;

namespace Logitar.Wishes.Application.Wishlists.Validators;

internal class UpdateItemPayloadValidator : AbstractValidator<UpdateItemPayload>
{
  public UpdateItemPayloadValidator()
  {
    When(x => !string.IsNullOrWhiteSpace(x.DisplayName),
      () => RuleFor(x => x.DisplayName!).SetValidator(new DisplayNameValidator())
    );

    When(x => !string.IsNullOrWhiteSpace(x.Summary?.Value),
      () => RuleFor(x => x.Summary!.Value!).SetValidator(new SummaryValidator())
    );

    When(x => !string.IsNullOrWhiteSpace(x.PictureUrl?.Value),
      () => RuleFor(x => x.PictureUrl!.Value!).SetValidator(new UrlValidator())
    );

    When(x => x.Price?.Value != null,
      () => RuleFor(x => x.Price!.Value!).SetValidator(new PricePayloadValidator())
    );

    When(x => x.Contents?.Value != null,
      () => RuleFor(x => x.Contents!.Value!).SetValidator(new ContentsPayloadValidator())
    );

    RuleForEach(x => x.Gallery).SetValidator(new UrlActionValidator());

    RuleForEach(x => x.Links).SetValidator(new UrlActionValidator());
  }
}
