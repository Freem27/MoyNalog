namespace TDV.MoyNalog.Models;

public class GetIncomesResponse
{
    public List<Income> Content { get; set; } = new();
    public int CurrentLimit { get; set; }
    public int CurrentOffset { get; set; }
    public bool HasMore { get; set; }
}
