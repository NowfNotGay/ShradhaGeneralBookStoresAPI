using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ShradhaGeneralBookStores.Converters;

public class DateTimeCoverter : JsonConverter<DateTime>
{
    private string formatDateTime = "dd/MM/yyyy HH:mm:ss";

    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return DateTime.ParseExact(reader.GetString(), formatDateTime, CultureInfo.InvariantCulture);
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(formatDateTime));
    }
}
