using FluentValidation;
using Logitar.Wishes.Contracts.Wishlists;
using Logitar.Wishes.Domain.Wishlists;

namespace Logitar.Wishes.Application.Wishlists.Validators;

internal class ContentsPayloadValidator : AbstractValidator<ContentsPayload>
{
  public ContentsPayloadValidator()
  {
    RuleFor(x => x.Text).SetValidator(new ContentTextValidator());

    RuleFor(x => x.Type).SetValidator(new ContentTypeValidator());
  }
}
