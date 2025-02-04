﻿using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace TDV.MoyNalog.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ReceiptType
{
    [EnumMember(Value = "REGISTERED")]
    REGISTERED,
    [EnumMember(Value = "CANCELLED")]
    CANCELLED,
}
