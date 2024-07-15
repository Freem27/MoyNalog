namespace MoyNalog.Models;

public class CancellationInfo
{
    public string Comment { get; set; } = null!;
    public int TaxPeriodId { get; set; }
    public DateTime OperationTime { get; set; }
    public DateTime RegisterTime { get; set; }
}
