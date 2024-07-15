namespace MoyNalog.Models;

public class TokenResponse
{
    public string RefreshToken { get; set; } = null!;
    public DateTime? RefreshTokenExpiresIn { get; set; }
    public string Token { get; set; } = null!;
    public DateTime? TokenExpireIn { get; set; }
}
