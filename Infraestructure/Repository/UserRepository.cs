using Domain.Entities;
using Infraestructure.Data;
using Infraestructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repository;

public class UserRepository: IUserRepository
{
    private readonly DataContext _dataContext;
    
    public UserRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    
    public async Task<UserEntity?> GetUserByEmail(string email)
    {
        return await _dataContext.UserEntity.FirstOrDefaultAsync(x => x.Email == email);
    }

    public void Update(UserEntity user)
    {
        _dataContext.Entry(user).State = EntityState.Modified;
    }


    public async Task<int> Save()
    {
        return await _dataContext.SaveChangesAsync();
    }
}