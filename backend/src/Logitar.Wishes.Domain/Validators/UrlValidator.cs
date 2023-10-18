using FluentValidation;
using Logitar.Wishes.Domain.Extensions;
using Logitar.Wishes.Domain.ValueObjects;

namespace Logitar.Wishes.Domain.Validators;

public class UrlValidator : AbstractValidator<string>
{
  public UrlValidator(string? propertyName = null)
  {
    RuleFor(x => x).NotEmpty()
      .MaximumLength(UrlUnit.MaximumLength)
      .Must(BeAValidUrl)
        .WithErrorCode(nameof(UrlValidator))
        .WithMessage("'{PropertyName}' must be a valid URL.")
      .WithPropertyName(propertyName);
  }

  private static bool BeAValidUrl(string url)
  {
    try
    {
      _ = new Uri(url);

      return true;
    }
    catch (Exception)
    {
      return false;
    }
  }
}
