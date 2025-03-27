using Infraestructure.Core.DTO.Auth;

namespace Domain.Services.Interfaces.Interfaces;

public interface IAuthService
{
    public Task<object> Login(AuthRequest model);
}