using TDV.MoyNalog.Enums;

namespace TDV.MoyNalog.Models;

public class AddIncomeRequest
{
    public Client Client { get; set; } = null!;
    public bool IgnoreMaxTotalIncomeRestriction { get; set; }
    public PaymentType PaymentType { get; set; } = PaymentType.CASH;
    public DateTime OperationTime { get; set; }
    public DateTime RequestTime => DateTime.UtcNow;
    public List<ServiceInfo> Services { get; set; } = new();
    public string TotalAmount => Math.Round(Services.Sum(x => x.Amount), 2).ToString().Replace(",", ".");

    public void Assert()
    {
        Client.Assert();

        if (Services.Count == 0)
        {
            throw new ApplicationException("Services.Count == 0");
        }

        if (Services.Any(s => s.Amount < 0))
        {
            throw new ApplicationException("Services.Any(s => s.Amount < 0)");
        }

        if (Services.Any(s => s.Quantity < 1))
        {
            throw new ApplicationException("Services.Any(s => s.Quantity < 1)");
        }

        if (OperationTime > DateTime.UtcNow)
        {
            throw new ApplicationException("OperationTime > DateTime.UtcNow");
        }
    }
}
