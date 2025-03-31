using Domain.DTO.Auth;

namespace Application.Services.Interfaces;

public interface IAuthService
{
    public Task<object> Login(AuthRequest model);
}