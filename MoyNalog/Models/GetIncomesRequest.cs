using MoyNalog.Enums;

namespace MoyNalog.Models;

public class GetIncomesRequest
{
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    public int Offset { get; set; } = 0;
    public IncomesSortBy SortBy { get; set; }
    public ReceiptType? ReceiptType { get; set; }
    public BuyerType? BuyerType { get; set; }
    public int Limit { get; set; }
}
