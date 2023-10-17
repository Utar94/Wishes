using FluentValidation;
using Logitar.Wishes.Domain.Extensions;

namespace Logitar.Wishes.Domain.Wishlists;

public class ContentTextValidator : AbstractValidator<string>
{
  public ContentTextValidator(string? propertyName = null)
  {
    RuleFor(x => x).NotEmpty()
      .WithPropertyName(propertyName);
  }
}
