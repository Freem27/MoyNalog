using System.Text.Json.Serialization;

namespace TDV.MoyNalog.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum IncomeType
{
    FROM_INDIVIDUAL,
    FROM_LEGAL_ENTITY,
}
