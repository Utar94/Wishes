using FluentValidation;
using Logitar.Wishes.Domain.Extensions;

namespace Logitar.Wishes.Domain.Validators;

public class UrlValidator : AbstractValidator<string>
{
  public const int MaximumLength = 2048;

  public UrlValidator(string? propertyName = null)
  {
    RuleFor(x => x).NotEmpty()
      .MaximumLength(MaximumLength)
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
