namespace MoyNalog.Models;

public class CancelIncomeRequest
{
    public DateTime OperationTime => DateTime.UtcNow;
    public string Comment { get; set; } = null!;
    public string ReceiptUuid { get; set; } = null!;
    public DateTime RequestTime { get; set; }
}
