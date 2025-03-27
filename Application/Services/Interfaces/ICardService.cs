using Infraestructure.Core.DTO.Card;

namespace Domain.Services.Interfaces.Interfaces;

public interface ICardService
{
    public Task<object> CreateCardAsync(CreateCardRequest model, int userId);
    
    public Task<object> GetCardBalanceAsync(string cardNumber, int userId);
}