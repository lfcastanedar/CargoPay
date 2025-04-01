using Domain.Entities;

namespace Infraestructure.Repository.Interfaces;

public interface IUserRepository
{
    public Task<UserEntity?> GetUserByEmail(string email);
    public void Update(UserEntity card);
    public Task<int> Save();
}