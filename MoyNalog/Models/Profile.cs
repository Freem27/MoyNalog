namespace TDV.MoyNalog.Models;

public class Profile
{
    public int Id { get; set; }
    public bool AvatarExists { get; set; }
    public bool RestrictedMode { get; set; }
    public bool HideCancelledReceipt { get; set; }
    public DateTime FirstReceiptCancelTime { get; set; }
    public DateTime FirstReceiptRegisterTime { get; set; }
    public DateTime InitialRegistrationDate { get; set; }
    public string DisplayName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? LastName { get; set; }
    public string? Login { get; set; }
    public string? MiddleName { get; set; }
    public string? PfrUrl { get; set; }
    public string Phone { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string Snils { get; set; } = string.Empty;
}
