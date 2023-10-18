using FluentValidation;
using Logitar.Wishes.Domain.Extensions;

namespace Logitar.Wishes.Domain.Wishlists;

internal class ItemIdValidator : AbstractValidator<string>
{
  public ItemIdValidator(string? propertyName = null)
  {
    RuleFor(x => x).NotEmpty()
      .MaximumLength(ItemId.MaximumLength)
      .WithPropertyName(propertyName);
  }
}
