using FluentValidation;
using Logitar.Wishes.Domain.Validators;

namespace Logitar.Wishes.Domain.ValueObjects;

public record UrlUnit
{
  public Uri Uri { get; }
  public string Value => Uri.ToString();

  public UrlUnit(string uriString)
  {
    new UrlValidator(nameof(Value)).ValidateAndThrow(uriString);

    Uri = new(uriString);
  }

  public static UrlUnit? TryCreate(string? uriString) => string.IsNullOrWhiteSpace(uriString) ? null : new(uriString.Trim());
}
