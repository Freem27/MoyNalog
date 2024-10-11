using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace TDV.MoyNalog.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum IncomesSortBy
{
    [EnumMember(Value = "operation_time:asc")]
    OperationTime,
    [EnumMember(Value = "operation_time:desc")]
    OperationTimeDesc,
    [EnumMember(Value = "total_amount:asc")]
    TotalAmount,
    [EnumMember(Value = "total_amount:desc")]
    TotalAmountDesc
}
