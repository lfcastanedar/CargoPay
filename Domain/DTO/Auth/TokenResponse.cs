namespace Infraestructure.Core.DTO.Auth;

public class TokenResponse
{
    public string Token { get; set; }
    public double Expiration { get; set; }
}