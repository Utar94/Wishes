﻿using FluentValidation;
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

    // TODO(fpion): Price

    // TODO(fpion): Rank/Priority

    // TODO(fpion): Contents

    // TODO(fpion): Gallery

    // TODO(fpion): Links
  }
}
