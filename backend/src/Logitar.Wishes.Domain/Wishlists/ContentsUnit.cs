using FluentValidation;

namespace Logitar.Wishes.Domain.Wishlists;

public record ContentsUnit
{
  public string Text { get; }
  public string Type { get; }

  public ContentsUnit(string text, string type = MediaTypeNames.Text.Plain)
  {
    new ContentTextValidator(nameof(Text)).ValidateAndThrow(text);
    new ContentTypeValidator(nameof(Type)).ValidateAndThrow(type);

    Text = text;
    Type = type;
  }
}
