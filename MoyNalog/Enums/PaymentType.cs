using System.Text.Json.Serialization;

namespace TDV.MoyNalog.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum PaymentType
{
    CASH,
}
