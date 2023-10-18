using Logitar.Wishes.Domain.ValueObjects;

namespace Logitar.Wishes.Infrastructure.Converters;

internal class DisplayNameUnitConverter : JsonConverter<DisplayNameUnit>
{
  public override DisplayNameUnit? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    string? value = reader.GetString();

    return value == null ? null : new DisplayNameUnit(value);
  }

  public override void Write(Utf8JsonWriter writer, DisplayNameUnit displayName, JsonSerializerOptions options)
  {
    writer.WriteStringValue(displayName.Value);
  }
}
