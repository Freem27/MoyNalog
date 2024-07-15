using MoyNalog.Enums;

namespace MoyNalog.Models;

public class Client
{
    public string? ContactPhone {  get; set; }
    public string? DisplayName {  get; set; }
    public IncomeType IncomeType { get; set; } = IncomeType.FROM_INDIVIDUAL;
    public string? Inn { get; set; }
    public void Assert()
    {
        switch (IncomeType)
        {
            case IncomeType.FROM_INDIVIDUAL:
                if (Inn != null)
                {
                    throw new ApplicationException("Inn != null");
                }
                break;
            case IncomeType.FROM_LEGAL_ENTITY:
                if (Inn == null)
                {
                    throw new ApplicationException("Inn == null");
                }
                break;
        }
    }
}