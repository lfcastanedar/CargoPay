using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Services.Interfaces;
using Domain.DTO.Auth;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Resources;
using Infraestructure.Core.Constans;
using Infraestructure.Repository.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services;

public class AuthService: IAuthService
{
    private readonly IConfiguration _configuration;
    private readonly IUserRepository _userRepository;

    public AuthService(IConfiguration configuration, IUserRepository userRepository)
    {
        _configuration = configuration;
        _userRepository = userRepository;
    }

    public async Task<object> Login(AuthRequest model)
    {
        string normalizedEmail = NormalizeEmail(model.Email);
        

        var user = await _userRepository.GetUserByEmail(normalizedEmail) 
                   ?? throw new BusinessException(GeneralMessages.BadCredentials);

        if (CanUserUnblock(user) && IsUserBlocked(user))
        {
            user.LoginAttempts = 0;
        }
        
        if (IsUserBlocked(user))
        {
            throw new BusinessException(GeneralMessages.UserBlocked);
        }
        

        if (!IsPasswordValid(model.Password, user.Password))
        {
            await HandleInvalidPassword(user);
        }

        ResetLoginAttempts(user);
        return GenerateTokenJwt(user);
    }

    private string NormalizeEmail(string email) => email.ToLower();

    private bool IsPasswordValid(string inputPassword, string storedPassword)
        => BCrypt.Net.BCrypt.Verify(inputPassword, storedPassword);

    private async Task HandleInvalidPassword(UserEntity user)
    {
        user.LoginAttempts++;
        user.LastLoginAttemptDate = DateTime.Now;
        await UpdateUserState(user);
        throw new BusinessException(GeneralMessages.BadCredentials);
    }

    private bool IsUserBlocked(UserEntity user)
    {
        const int maxLoginAttempts = 5;

        return user.LoginAttempts >= maxLoginAttempts;
    }
    
    private bool CanUserUnblock(UserEntity user)
    {
        const int maxLoginAttempts = 5;
        var timeDifference = DateTime.Now - user.LastLoginAttemptDate;

        return timeDifference.TotalMinutes >= 5;

    }

    private void ResetLoginAttempts(UserEntity user)
    {
        user.LoginAttempts = 0;
        _userRepository.Update(user);
        _userRepository.Save();
    }

    private async Task UpdateUserState(UserEntity user)
    {
        _userRepository.Update(user);
        await _userRepository.Save();
    }

    
    private TokenResponse GenerateTokenJwt(UserEntity user)
    {
        IConfigurationSection tokenAppSetting = _configuration.GetSection("JWT");

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenAppSetting.GetSection("Key").Value));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
        var header = new JwtHeader(signingCredentials);
        

        var claims = new[] {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(TypeClaims.Email,user.Email),
            new Claim(TypeClaims.UserId, user.Id.ToString()),
            
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
            Token = new JwtSecurityTokenHandler().WriteToken(token)
        };

        return tokenResponse;
    }
}