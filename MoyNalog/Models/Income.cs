using MoyNalog.Enums;

namespace MoyNalog.Models;

public class Income : IncomeInfo
{
    public string? ClientDisplayName { get; set; }
    public string? ClientInn { get; set; }
    public string? Email { get; set; }
    public List<string> Description { get; set; } = new();
    public string Inn { get; set; } = null!;
    public IncomeType IncomeType { get; set; }
    public string? InvoiceId { get; set; }
    public string? PartnerDisplayName { get; set; }
    public string? PartnerInn { get; set; }
    public string? PartnerLogo { get; set; }
    public string Profession { get; set; } = string.Empty;
    public List<Service> Services { get; set; } = new();
    public int TaxPeriodId { get; set; }
    public DateTime RegisterTime { get; set; }
}
