using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Services.Interfaces.Interfaces;
using Infraestructure.Core.Constans;
using Infraestructure.Core.DTO.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Domain.Services.Interfaces;

public class AuthService: IAuthService
{
    private readonly IConfiguration _configuration;

    public AuthService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<object> Login(AuthRequest model)
    {
        return GenerateTokenJwt(model);
    }
    
    
    private TokenResponse GenerateTokenJwt(AuthRequest user)
    {
        IConfigurationSection tokenAppSetting = _configuration.GetSection("JWT");

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenAppSetting.GetSection("Key").Value));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
        var header = new JwtHeader(signingCredentials);
        

        var claims = new[] {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(TypeClaims.Email,user.Email),
            new Claim(TypeClaims.UserId, "1"),
            
        };

        int delayExpiration = int.Parse(tokenAppSetting.GetSection("TokenExpirationMinutes").Value ?? "0");
        
        var payload = new JwtPayload(
            issuer: tokenAppSetting.GetSection("Issuer").Value,
            audience: tokenAppSetting.GetSection("Audience").Value,
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddMinutes(delayExpiration)
        );

        var token = new JwtSecurityToken(
            header,
            payload
        );

        TokenResponse tokenResponse = new TokenResponse
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            //Expiration = Helper.ConvertToUnixTimestamp(_token.ValidTo),
        };

        return tokenResponse;
    }
}