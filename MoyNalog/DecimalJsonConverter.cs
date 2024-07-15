using System.Text.Json.Serialization;
using System.Text.Json;
namespace MoyNalog;

public class DecimalJsonConverter : JsonConverter<decimal>
{
    public override decimal Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        decimal.TryParse(reader.GetString(), out decimal result);
        return result;
    }

    public override void Write(Utf8JsonWriter writer, decimal value, JsonSerializerOptions options)
    {
        writer.WriteRawValue(value.ToString().Replace(",","."));
    }
}
