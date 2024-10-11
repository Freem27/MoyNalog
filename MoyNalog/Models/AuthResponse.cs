namespace TDV.MoyNalog.Models;

public class AuthResponse : TokenResponse
{
    public Profile Profile { get; set; } = null!;
}
