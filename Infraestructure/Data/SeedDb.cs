using Domain.Entities;

namespace Infraestructure.Data;

public class SeedDb
{
    private readonly DataContext _context;
    
    public SeedDb(DataContext context)
    {
        _context = context;
    }
    
    public async Task ExecSeedAsync()
    {
        await CheckUserAsync();
    }
    
    private async Task CheckUserAsync()
    {
        if (!_context.UserEntity.Any())
        {
            UserEntity user = new UserEntity()
            {
                Email = "email@email.com",
                Password = "$2a$12$h71VrCwCpPwYQuxl4yRZDeLMjNTSQFaIvzekhDflSwlwSSTQ4nXQi",
                LoginAttempts = 0,
                LastLoginAttemptDate = DateTime.Now
            };

            _context.UserEntity.Add(user);

            await _context.SaveChangesAsync();
        }
    }
}