using System.Text.Json.Serialization;

namespace MoyNalog.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum PaymentType
{
    CASH,
}
