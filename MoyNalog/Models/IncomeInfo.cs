using TDV.MoyNalog.Enums;

namespace TDV.MoyNalog.Models;

public class IncomeInfo
{
    public string ApprovedReceiptUuid { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string SourceDeviceId { get; set; } = null!;
    public string? PartnerCode { get; set; }
    public PaymentType PaymentType { get; set; }
    public DateTime OperationTime { get; set; } = DateTime.UtcNow;
    public DateTime RequestTime { get; set; } = DateTime.UtcNow;
    public CancellationInfo? CancellationInfo { get; set; }
    public decimal TotalAmount { get; set; }
}
