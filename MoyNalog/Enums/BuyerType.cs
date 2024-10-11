using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace TDV.MoyNalog.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum BuyerType
{
    [EnumMember(Value = "PERSON")]
    PERSON,
    [EnumMember(Value = "FOREIGN_AGENCY")]
    FOREIGN_AGENCY,
}
