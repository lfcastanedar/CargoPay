using Domain.DTO.Card;

namespace Application.Services.Interfaces;

public interface ICardService
{
    public Task<CardResponse> CreateCardAsync(CreateCardRequest model, int userId);
    
    public Task<CardBalanceResponse?> GetCardBalanceAsync(string cardNumber, int userId);
}