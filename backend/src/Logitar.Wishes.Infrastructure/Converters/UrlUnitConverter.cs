using Logitar.Wishes.Domain.ValueObjects;

namespace Logitar.Wishes.Infrastructure.Converters;

internal class UrlUnitConverter : JsonConverter<UrlUnit>
{
  public override UrlUnit? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    string? value = reader.GetString();

    return value == null ? null : new UrlUnit(value);
  }

  public override void Write(Utf8JsonWriter writer, UrlUnit url, JsonSerializerOptions options)
  {
    writer.WriteStringValue(url.Value);
  }
}
