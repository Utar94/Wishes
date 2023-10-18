using Logitar.Wishes.Domain.Wishlists;

namespace Logitar.Wishes.Infrastructure.Converters;

internal class ItemIdConverter : JsonConverter<ItemId>
{
  public override ItemId? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    string? value = reader.GetString();

    return value == null ? null : new ItemId(value);
  }

  public override void Write(Utf8JsonWriter writer, ItemId id, JsonSerializerOptions options)
  {
    writer.WriteStringValue(id.Value);
  }
}
