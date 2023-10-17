using FluentValidation;
using Logitar.Wishes.Domain.Extensions;
using System.Net.Mime;

namespace Logitar.Wishes.Domain.Wishlists;

public class ContentTypeValidator : AbstractValidator<string>
{
  private static readonly HashSet<string> _validTypes = new(new string[]
  {
    MediaTypeNames.Text.Html,
    MediaTypeNames.Text.Plain,
    "text/markdown"
  });

  public static IEnumerable<string> SupportedTypes => _validTypes.ToArray();

  public ContentTypeValidator(string? propertyName = null)
  {
    RuleFor(x => x).NotEmpty()
      .Must(_validTypes.Contains)
        .WithErrorCode(nameof(ContentTypeValidator))
        .WithMessage($"'{{PropertyName}}' must be one of the following: {string.Join(", ", _validTypes)}.")
      .WithPropertyName(propertyName);
  }
}
