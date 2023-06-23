using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BaiThiWEBAPI.Converters;

public class DateConverter : JsonConverter<DateTime>
{
    private string formateDate = "dd/MM/yyyy";
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return DateTime.ParseExact(reader.GetString()   , formateDate, CultureInfo.InvariantCulture);
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(formateDate));
    }
}
