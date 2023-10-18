using Logitar.Wishes.Domain.Wishlists;

namespace Logitar.Wishes.Infrastructure.Converters;

internal class SummaryUnitConverter : JsonConverter<SummaryUnit>
{
  public override SummaryUnit? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    string? value = reader.GetString();

    return value == null ? null : new SummaryUnit(value);
  }

  public override void Write(Utf8JsonWriter writer, SummaryUnit summary, JsonSerializerOptions options)
  {
    writer.WriteStringValue(summary.Value);
  }
}
